# Unity-Simple-Astar-Path-Finding
Unity Simple Astar Pseudo
ASTAR PSEUDO
OPEN_LIST  -  LIST HOLDING UNCHECKED NODE
CLOSED_LIST - LIST HOLDING CHECKED NODE
NODE_START  - START POINT
NODE_END - TARGET POINT
NODE_CURRENT - CURRENT CHECKED NODE
FCOST = GCOST + HCOST
HCOST = DISTANCE FROM CURRENT_NODE TO NODE_END
GCOST  = DISTANCE FROM CURRENT_NODE TO NODE_START
NODE_PARENT  : PARENT OF SOME_NODE
 
PARENT:A NODE_START IS A ROOT_NODE , ROOT_NODE WILL“BORN” A CHILD_NODE , ROOT_NODE IS A PARENT OF CHILD_NODE , CHILD_NODE ALSO BORN ANOTHER_NODE, ANOTHER_NODE IS CHILD OF CHILD_NODE YOU MIGHT THINKING ABOUT  TREE , ROOT_NODE NEVER HAVE PARENT.
 
EXAMPLE :
NEIGHBOUR OF SOME_NODE = NODE AROUND SOME_NODE , CAN BE CONNECTED IN THE GRAPH, EXAMPLE SOME_NODE POINT (0,0), SOME_NODE NEIGHBOUR IS:
(0,1) TOP 			(0,-1) DOWN 		
(1,0) 	RIGHT		 (-1,0) LEFT			 
{-1,1) TOP_LEFT 		(-1,-1) BOTTOM_LEFT	 
(1,1)	 TOP_RIGHT		 (1,-1) BOTTOM_RIGHT	 
Algorithms 
ADD NODE START TO OPEN_LIST  
WHILE OPEN_LIST IS NOT EMPTY 
 	 LOOP
NODE_CURRENT = NODE IN OPEN_LIST WITH LOWEST FCOST
ADD IT TO CLOSED_LIST 
REMOVE IT FROM OPEN_LIST
		
IF NODE_CURRENT IS A NODE_END
		FOUND PATH AND RETRACE THE PATH
	ELSE
			LOOP THROUGH NEIGHBOUR OF NODE_CURRENT
					IF CLOSED_LIST NOT CONTAINS NEIGHBOUR  
					IF OPEN_LIST  NOT CONTAINS NEIGHBOUR
							ADD NEIGHBOUR TO OPEN_LIST	
SET NODE_PARENT OF NEIGHBOUR TO NODE_CURRENT
ENDLOOP
			
	
PATH
Algorithms 
RETRACE	
SET NODE_CURRENT TO NODE_END
	WHILE NODE_CURRENT IS NOT NODE_START
LOOP
			ADD NODE_CURRENT TO PATH
	SET NODE_CURRENT TO PARENT_NODE OF NODE_CURRENT
	
 
 
ENDLOOP
ADD NODE_CURRENT TO PATH
REVERSE PATH
 
 
 
