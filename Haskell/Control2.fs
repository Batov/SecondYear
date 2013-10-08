--Batov/271

fromFun :: (Integer -> Integer) -> [Integer] -> [(Integer,Integer)]
fromFun f a = [(x,f x) | x <- a]

dom :: [(Integer,Integer)] -> [Integer]
dom a = [ fst x | x <- a]

eval :: [(Integer,Integer)] -> Integer -> Integer
eval [] x = error "fail"
eval ((xa,ya):xs) x = if x == xa then ya else eval xs x

invert :: [(Integer,Integer)] -> [(Integer,Integer)]
invert a = [(y,x) | (x,y) <- a]  

(.*.) :: [(Integer,Integer)] -> [(Integer,Integer)] -> [(Integer,Integer)]
b .*. a = [(fst a1,snd b1) | a1 <- a, b1 <- b, snd a1 == fst b1]
infixl 9 .*.

image :: [(Integer,Integer)] -> [Integer] -> [Integer]
image fl xl = [ snd f | f <- fl, x <- xl, fst f == x] 

preimage :: [(Integer,Integer)] -> [Integer] -> [Integer]
preimage fl yl = [fst f | f <- fl , y <- yl, snd f == y]

isInjective :: [(Integer,Integer)] -> Bool
isInjective fl = length [False |(x1,y1) <- fl , (x2,y2) <- fl, y2 == y1 && x2 /= x1] == 0 

isSurjective :: [(Integer,Integer)] -> Bool
isSurjective _ = True
--Область значений табличной функции это все y = snd f
--Для любого y мы найдем x

areMutuallyInverse :: [(Integer,Integer)] -> [(Integer,Integer)] -> Bool
areMutuallyInverse f1l f2l = f1l == invert f2l 
