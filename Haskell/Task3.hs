conc l = foldl (++) [] l

listify' (x:xs) = [x]:listify' xs 
listify' [] = []

listify1 = map (:[])

listify'' = foldl (\ x y -> x ++[[y]]) []


units =  1 : units

naturals = 1 : []

isPrime a  = (divisors a == [1]) where 
					divisors n = [x | x <- [1..(n `div` 2)], n `rem` x == 0]

primes = 2 : filter isPrime [3,5..]

primes' = [x | x <- [3,5..], isPrime x]


fibs1 = [x | x <- [1..], isSquare (5*x*x+4) || isSquare (5*x*x- 4)]
	where 
		isSquare x = let x' = truncate $ sqrt (fromIntegral x :: Double) in square x' == x where square x = x*x
	

fibs2 n = fibs' [] 0 1 n 
	where
		fibs' [] _ _ 0 = []
		fibs' [] _ _ 1 = [1]
		fibs' x _ _ 0 = x
		fibs' x a b n = fibs' (x++[b]) b (a+b) (n-1)


		
		
