fact (n)
  | n==0 = 1
  | n>0 = n * fact(n-1)

isPrime a  = (divisors a == [1]) where 
					divisors n = [x | x <- [1..(n `div` 2)], n `rem` x == 0] 





mygcd x y 
	| ((x `rem` y) == 0) = y
	| otherwise = mygcd y (x `rem` y)

mylcm x y = abs(x*y) `div` (mygcd x y)

mycpr x y = (abs(mygcd x y) == 1)

myeul n = foldl (+) 0 [cpr n x | x <- [1..n]] 
			where 
				cpr x y
					| (abs(mygcd x y) == 1) = 1
					| otherwise = 0