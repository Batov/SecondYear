--List functions

len [] = 0
len (x:xs) = 1 + len xs

pushth a [] = [a]
pushth a (x:xs) = (x:(pushth a xs))

rev [] = []
rev (x:xs) = pushth x ( rev xs) 

con [] a = a
con (x:xs) a = x:(con xs a)

_take _ [] = []
_take 0 _ = []
_take a (x:xs) = x:_take (a-1) xs

myconcat [] = []
myconcat ((x:xs):xs2) = (x:xs) ++ (myconcat xs2) 

add [a] b = (a:b) 
add (a:b) (c:d) = a:(add b (c:d))

infixl 7 ***
a *** b = myconcat (f a b)
	where 
			f _ [] = []
			f [] _ = []
			f (x:xs) ys = map (* x) ys : f xs ys


--Fib (pairs)

fib n = fib' n (0,1) 
	where
		fib' 0 (a,_) = a 
		fib' n (a,b) = fib' (n-1) (b,a+b)


--fraction

reduce (x,y) = (div x (gcd x y), div y (gcd x y))

infixl 7 *\
(x1,y1) *\ (x2,y2) = reduce (x1*x2, y1*y2)

infixl 7 /\
(x1,y1) /\ (x2,y2) = reduce (x1*y2, y1*x2)

infixl 6 +\ 
(x1,y1) +\ (x2,y2) = reduce (x1*y2 + x2 * y1 , y1 * y2)

infixl 6 -\
(x1,y1) -\ (x2,y2) = reduce (x1*y2 - x2 * y1 , y1 * y2)

isZero (x, _) = x == 0

--polynom

addMon [] m = [m] 
addMon p@((pn,pe):xs) (mn,me) = 
	if me < pe then (pn,pe) : addMon xs (mn,me)
	else if me > pe then (mn,me) : p 
		  else if isZero summ 
		  	then xs else (summ,pe) :xs 
		  	where summ = mn +\ pn	

subMon polynom ((a,b),e) = addMon polynom ((-a,b),e)

mulMon [] _ = []
mulMon _ ((0,_),_) = []
mulMon ((pn,pe):xs) m@(mn,me) = (pn *\ mn,pe+me):mulMon xs m

addPol p []  = p
addPol p (x:xs) = addPol (addMon p x) xs  

subPol p []  = p
subPol p (x:xs) = subPol (subMon p x) xs  

mulPol _ [] = []
mulPol [] _ = []
mulPol p  (x:xs) = addPol (mulMon p x) (mulPol p xs)


lessThMon (_, e) (_, e2) = e < e2

lessThPol [] [] = False
lessThPol _ [] = False
lessThPol [] _ = True
lessThPol (x:xs) (x':xs') = (lessThMon x x' || (x == x' && lessThPol xs xs'))

divMon (n, e) (n', e') = (n /\ n, e - e')


divPol divend divor@(x:_) =
    divide [] divend
    where divide a [] = (a, [])
          divide a r@(y:_) = 
            if lessThPol r divor then (a, r) else divide a' r'
            where
             	  r' = subPol r (mulMon divor (divMon y x))
                  a' = addMon a (divMon y x)

