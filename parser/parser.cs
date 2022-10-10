public class Parser {
  Lexer l;
  List<string> errors = new List<string>();

  Token curToken = new(Token.ILLEGAL, "");
  Token peekToken = new(Token.ILLEGAL, "");
  
  Dictionary<TokenType, prefixParseFn> prefixParseFns = new();
  Dictionary<TokenType, infixParseFn> infixParseFns = new();

  Dictionary<TokenType, OperatorPrecedences> precedences = new()
  {
    {Token.EQ, OperatorPrecedences.EQUALS},
    {Token.NOT_EQ, OperatorPrecedences.EQUALS},
    {Token.LT, OperatorPrecedences.LESSGREATER},
    {Token.GT, OperatorPrecedences.LESSGREATER},
    {Token.PLUS, OperatorPrecedences.SUM},
    {Token.MINUS, OperatorPrecedences.SUM},
    {Token.SLASH, OperatorPrecedences.PRODUCT},
    {Token.ASTERISK, OperatorPrecedences.PRODUCT},
  };

  enum OperatorPrecedences
  {
    LOWEST,
    EQUALS,
    LESSGREATER,
    SUM,
    PRODUCT,
    PREFIX,
    CALL,
  }

  public delegate Expression prefixParseFn();
  public delegate Expression infixParseFn(Expression e);

  public Parser(Lexer l) {
    this.l = l;

    nextToken();
    nextToken();

    registerPrefix(Token.IDENT, parseIdentifier);
    registerPrefix(Token.INT, parseIntegerLiteral);
    registerPrefix(Token.BANG, parsePrefixExpression);
    registerPrefix(Token.MINUS, parsePrefixExpression);
    registerPrefix(Token.TRUE, parseBoolean);
    registerPrefix(Token.FALSE, parseBoolean);
    
    registerInfix(Token.PLUS, parseInfixExpression);
    registerInfix(Token.MINUS, parseInfixExpression);
    registerInfix(Token.SLASH, parseInfixExpression);
    registerInfix(Token.ASTERISK, parseInfixExpression);
    registerInfix(Token.EQ, parseInfixExpression);
    registerInfix(Token.NOT_EQ, parseInfixExpression);
    registerInfix(Token.LT, parseInfixExpression);
    registerInfix(Token.GT, parseInfixExpression);
  }

  public List<string> Errors() {
    return errors;
  }

  public void registerPrefix(TokenType tokenType, prefixParseFn fn) {
    prefixParseFns[tokenType] = fn;
  }

  public void registerInfix(TokenType tokenType, infixParseFn fn) {
    infixParseFns[tokenType] = fn;
  }

  public void peekError(TokenType t) {
    var msg = string.Format("peek expected {0}, got {1}", t, peekToken.Type);
    errors.Add(msg);
  }

  public void nextToken() {
    curToken = peekToken;
    peekToken = l.NextToken();
  }

  Statement? parseStatement() {
    switch (curToken.Type) {
      case Token.LET:
        return parseLetStatement();
      case Token.RETURN:
        return parseReturnStatement();
      default:
        return parseExpressionStatement();;
    }
  }

  bool expectPeek(TokenType t) {
    if (peekToken.Type == t) {
      nextToken();
      
      return true;
    }
    else {
      peekError(t);

      return false;
    }
  }

  Statement? parseLetStatement() {
    var token = curToken;

    if (!expectPeek(Token.IDENT)) {
      return null;
    }

    var ident = curToken;

    if (!expectPeek(Token.ASSIGN)) {
      return null;
    }

    nextToken();
    var num = curToken;

    while (curToken.Type != Token.SEMICOLON) {
      nextToken();
    }

    return new LetStatement(
      token,
      new Identifier(ident, ident.Literal),
      new Identifier(num, num.Literal)
    );
  }

  Statement? parseReturnStatement() {
    var t = curToken;

    nextToken();

    if (curToken.Type == Token.SEMICOLON) {
      return new ReturnStatement(t, null);
    }

    var num = curToken;

    while (curToken.Type != Token.SEMICOLON) {
      nextToken();
    }

    return new ReturnStatement(
      t,
      new Identifier(num, num.Literal)
    );
  }

  Statement? parseExpressionStatement() {
    var s = new ExpressionStatement(curToken, parseExpression(OperatorPrecedences.LOWEST));

    if (peekToken.Type == Token.SEMICOLON) {
      nextToken();
    }

    return s;
  }

  Expression? parseExpression(OperatorPrecedences precedence) {
    if (!prefixParseFns.TryGetValue(curToken.Type, out var prefix)) {
      errors.Add($"no prefix parse function for {curToken.Type} found");

      return null;
    }

    var leftExp = prefix();

    while ((peekToken.Type != Token.SEMICOLON) && (precedence < peekPrecedence())) {
      if (!infixParseFns.TryGetValue(peekToken.Type, out var infix)) {
        return leftExp;
      }

      nextToken();

      leftExp = infix(leftExp);
    }

    return leftExp;
  }

  Expression parseIdentifier() {
    return new Identifier(curToken, curToken.Literal);
  }

  Expression parseIntegerLiteral() {
    return new IntegerLiteral(curToken, Convert.ToInt64(curToken.Literal));
  }

  Expression parsePrefixExpression() {
    var t = curToken;

    nextToken();

    var e = parseExpression(OperatorPrecedences.PREFIX);

    return new PrefixExpression(t, t.Literal, e);
  }

  Expression parseBoolean() {
    var v = curToken.Type == Token.TRUE ? true : false;
    
    return new Boolean(curToken, v);
  }

  OperatorPrecedences peekPrecedence() {
    if (precedences.TryGetValue(peekToken.Type, out var p)) {
      return p;
    }

    return OperatorPrecedences.LOWEST;
  }

  OperatorPrecedences curPrecedence() {
    if (precedences.TryGetValue(curToken.Type, out var p)) {
      return p;
    }

    return OperatorPrecedences.LOWEST;
  }

  Expression parseInfixExpression(Expression left) {
    var t = curToken;
    var p = curPrecedence();
    nextToken();
    var r = parseExpression(p);

    return new InfixExpression(t, left, t.Literal, r);
  }

  public _Program ParseProgram() {
    var program = new _Program();

    while (curToken.Type != Token.EOF) {
      var s = parseStatement();

      if (s != null) {
        program.Statements.Add(s);
      }

      nextToken();
    }

    return program;
  }
}