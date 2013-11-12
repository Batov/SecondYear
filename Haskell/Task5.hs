data Tree a = Empty | Node a Integer (Tree a) (Tree a) deriving Show

insert Empty x = Node x 1 Empty Empty
insert nd@(Node a h l r) x
	| a > x = setH (Node a h (insert l x) r)
	| a < x = setH (Node a h l (insert r x))
	| otherwise = nd

setH (Node a _ l r) = Node a (max (getH l) (getH r) +1) l r
setH Empty = Empty

getH (Node _ h _ _) = h
getH Empty = 0

-- 4 type of rotate
balance nd@(Node a h l r)
	| (dbh nd)-1 > 0 && dbh r >  0 = lR nd
	| (dbh nd)-1 > 0 && dbh r <= 0 = lR (Node a h l (rR r))
	| (dbh nd)+1 < 0 && dbh l <  0 = rR nd
	| (dbh nd)+1 < 0 && dbh l >= 0 = rR (Node a h (lR l) r)
	| otherwise = nd

  where
		

	lR (Node a h (Node al hl ll lr) r) = setH (Node al 0 ll (setH (Node a 0 lr r)))
	lR Empty = Empty 
	rR (Node a h l (Node ar hr rl rr)) = setH (Node ar 0 (setH (Node a 0 l rl)) rr)
	rR Empty = Empty

	dbh (Node _ _ l r) = (getH l) - (getH r)	
	dbh Empty = 0
	
