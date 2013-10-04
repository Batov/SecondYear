data Tree a   = Empty | Node a (Tree a) (Tree a) deriving Show
--data Maybe	a = Just a | Nothing

insert Empty x = Node x Empty Empty
insert (Node a l r) x
	| a > x = Node a (insert l x) r
	| a < x = Node a l (insert r x)
	| otherwise = Node a l r

elements l = f l []
	where 
		f Empty lst = lst
		f (Node a l r) lst  = f l (a:f r lst)

isBST (Node a p@(Node n l r) Empty) = a > n && isBST p
isBST (Node a Empty p@(Node n l r)) = a < n && isBST p
isBST (Node a p@(Node n l r) q@(Node n2 l2 r2)) = a > n && a < n2 && isBST p && isBST q
isBST (Node a _ _ ) = True
isBST Empty = True

find Empty x = Nothing
find (Node a l r) x 
	| a == x 	= Just a
	| a > x 	= find l x
	| a < x 	= find r x