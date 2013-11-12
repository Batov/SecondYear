--Nikita Batov. Math. 2013.

type Parser a = String -> [(a, String)]

empty:: Parser a
empty _ = []

sym :: Char -> Parser Char
sym c (x:xs) | x == c = [(c,xs)]
sym _ _               = []

val :: a -> Parser a
val a s = [(a,s)]

infixl 2 |||
(|||) :: Parser a -> Parser a -> Parser a
p1 ||| p2 = \x -> (p1 x) ++ (p2 x)

infixl 3 ||>
(||>) :: Parser a -> (a -> Parser b) -> Parser b
p ||> q = \s -> concat [q a s | (a, s) <- p s]

many :: Parser a -> Parser [a]
many p = p ||> (\x -> many p ||> val . (x:)) ||| val []

opt :: Parser a -> Parser (Maybe a)
opt a = a ||> val . Just ||| val Nothing

eof :: [(a,String)] -> [a]
eof = map fst.filter ((==[]).snd)

data E = X String  |
		 N Integer | 
		 Mul E E   | 
		 Div E E   | 
		 Add E E   | 
		 Sub E E   | 
		 Eq E E    | 
		 Mr E E    | 
		 Me E E    |
	     Ls E E    | 
	     Le E E    | 
	     Ne E E    | 
	     And E E   | 
	     Or E E deriving Show

oneOf = foldl (\ a b -> a ||| sym b) empty

letter = oneOf "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz"
digit  = oneOf "0123456789"

ident =
	letter ||> (\x -> many (letter ||| digit) ||> (\xs -> val $ X (x:xs)))

literal = digit ||> (\x -> many digit ||> (\xs -> val $ N $ read(x:xs)))

brackets = sym '(' ||> (\_ -> expr ||> (\e -> sym ')' ||> (\_ -> val e)))

primary = 
	ident 	|||
	literal |||
	brackets

func' p op = p ||> (\x -> (many (op ||> (\o -> p ||> (\y -> val $ (o, y))))) 
                               ||> (\xs -> val $ foldl (\acc (o, y) -> acc `o` y) x xs))

--func ps op = ps ||> (\x -> op ||> (\o -> ps ||> (\y -> val $ x `o` y))) ||| ps

la ps op = func' ps op


multi = la primary op
	where
		op = sym '*' ||> (\ _ -> val Mul) |||
			 sym '/' ||> (\ _ -> val Div)


addi = la multi op
	where
		op = sym '+' ||> (\ _ -> val Add) |||
			 sym '-' ||> (\ _ -> val Sub)

bool = la addi op	
	 where
		op = sym '=' ||> (\ _ -> val Eq) |||
			 sym '>' ||> (\ _ -> val Mr) |||
			 sym '>' ||> eq ||> (\ _ -> val Me) |||
			 sym '<' ||> eq ||> (\ _ -> val Le) |||
			 sym '<' ||> (\ _ -> val Ls) |||
		     sym '!' ||> eq ||> (\ _ -> val Ne) 
		 	where eq = (\_ -> sym '=')


logi = la bool op	
	 where
		op = sym '&' ||> (\ _ -> val And) |||
			 sym '|' ||> (\ _ -> val Or)

expr = logi


test1 = eof $ expr "1+2+4-234*4"
test2 = eof $ expr "12/4&3*4"
test3 = eof $ expr "12-3-4"
test4 = eof $ expr "12!=30"
test5 = eof $ expr "12|43>=43"
test6 = eof $ expr "abc&asd+awe"