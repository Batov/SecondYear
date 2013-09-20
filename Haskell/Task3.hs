
isPrime a  = (divisors a == [1]) 
	where 
		divisors n = [x | x <- [1..(n `div` 2)], n `rem` x == 0]

primes = 2 : filter isPrime [3,5..]

primes' = 2 : [x | x <- [3,5..], isPrime x]

primes'' = 2 : f [3,5..]  
	where
   		f (p:xs) = p : f [x | x <- xs, x `rem` p /= 0]

fibs = [x | x <- [1..], isSquare (5*x*x+4) || isSquare (5*x*x- 4)]
	where 
		isSquare x = let x' = truncate $ sqrt (fromIntegral x :: Double) in square x' == x where square x = x*x

fibs' = 0:1:zipWith (+) fibs' (tail fibs')		
