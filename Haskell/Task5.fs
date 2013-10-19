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

lR (Node a h (Node al hl ll lr) r) = setH (Node al 0 ll (setH (Node a 0 lr r))) 
rR (Node a h l (Node ar hr rl rr)) = setH (Node ar 0 (setH (Node a 0 l rl)) rr)

dbh (Node _ _ l r) = (getH l) - (getH r)
dbh Empty = 0

balance nd@(Node a h l r)
	| dbh nd -1 > 0 && dbh r >  0 = setH $ lR nd
	| dbh nd -1 > 0 && dbh r <= 0 = setH $ lR (Node a h l (rR r))
	| dbh nd +1 < 0 && dbh l <  0 = setH $ rR nd
	| dbh nd +1 < 0 && dbh r >= 0 = setH $ rR (Node a h (lR l) r)
	| otherwise = nd

