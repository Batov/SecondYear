--Batov Nikita 271.

data M key value = E | T (key,[value]) Integer (M key value) (M key value) deriving Show

--TOOLS----------------------------------------------------------

getH E = 0
getH (T _ h _ _) = h

getData E = (0,[])
getData (T a _ _ _) = a

elems (T (k, v:_) _ l r) acc = elems l $ (k,v):elems r acc
elems _ acc = acc

make a l r = T a (max (getH l) (getH r) + 1) l r

dbh (T _ _ l r) = (getH r) - (getH l)

rR (T key _ (T lk _ ll lr) r) = make lk ll (make key lr r)
lR (T key _ l (T rk _ rl rr)) = make rk (make key l rl) rr

balance p@(T k h l r) =
    if dbh p > 1  then lR $ make k l (if dbh r > 0 then r else rR r) else 
    if dbh p < -1 then rR $ make k (if dbh l < 0 then l else lR l) r else p

--TASK-----------------------------------------------------------

empty = E

insert E key value = T (key,[value]) 1 E E
insert nd@(T p@(k,vs) h l r) key value
    | key > k     = balance $ make p l (insert r key value)
    | key < k     = balance $ make p (insert l key value) r
    | otherwise   = T (k,value:vs) h l r

find E key       	 = Nothing
find (T (k,vs:_) _ l r) key 
	| key < k = find l key
	| key > k = find r key
	| otherwise   = Just vs

remove E _ = E
remove nd@(T p@(k,(v:vs)) h l r) key
	| key > k   = balance $ make p l (remove r key)
	| key < k   = balance $ make p (remove l key) r
	| otherwise = if vs == [] then balance $ make k' l r' else T (k,vs) h l r
		where 
			(k',r') = chs r
			chs (T p _ E r) = (p,r)
			chs (T p _ l r) = (k', balance $ make p l' r) where (k',l') = chs l


fold f t acc = foldr (\(k, v) acc' -> f k v acc') acc $ elems t []

--TEST-----------------------------------------------------------

test  = (insert (insert (insert (insert E 2 10) 3 40) 2 30) 1 20) 
test2 = (insert (insert (insert (insert (insert (insert (insert (insert E 2 10) 3 4) 20 3) 1 2) 2 10) 8 4) 2 3) 1 2) 
test3 = (fold (\ _ b c  -> b:c) test []) == ([20,30,40])
test4 = (fold (\ a _ c  -> a:c) test []) == ([1,2,3])
test5 = (remove test 2)
test6 = find test2 3 == Just 4 
