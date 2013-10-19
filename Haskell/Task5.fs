data Tree a = Empty | Node a Integer (Tree a) (Tree a) deriving Show

insert Empty x = Node x 1 Empty Empty
insert (Node a h l r) x
	| a > x = Node a h (insert l x) r
	| a < x = Node a h l (insert r x)
	| otherwise = Node a h l r


getH Empty = 0
getH (Node _ h _ _) = h

setH Empty = Empty
setH (Node a _ l r) = Node a (max (getH l) (getH r) +1) l r

LR (Node a h (Node al hl ll lr) r) = setH (Node al -1 ll (setH (Node a -1 lr r))) 
RR (Node a h l (Node ar hr rl rr)) = setH (Node ar -1 (setH (Node a -1 l rl)) rr)

--balance (Node a h l@(Node al hl ll rl) r@(Node ar hr lr rr)) 
--	| hr+1<hl = Node al hl (balance ll) (balance (Node a hl-1 rl r))
--	| hl+1<hr = Node ar hr (balance (Node a hr-1 l lr)) (balance rr)
--	| otherwise = Node a h (balance l) (balance r)
--balance (Node a h (Node al hl ll rl) Empty) = Node al h (balance ll) (balance (Node a hl rl Empty))
--balance (Node a h Empty (Node ar hr lr rr)) = Node ar h (balance (Node a hr Empty lr)) (balance rr)
--balance Empty = Empty
