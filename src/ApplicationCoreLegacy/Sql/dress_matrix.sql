create table "dress_matrix"
(
	"id"				integer			not null primary key,
	"name"				varchar(255)	not null collate UTF8CI unique check(TRIM("name") <> ''),
	"cells_x"			varchar(2000)	not null collate UTF8CI,
	"cells_y"			varchar(2000)	not null collate UTF8CI
);
insert into "dress_matrix" ("id", "name", "cells_x", "cells_y") values (null, 'нет', '', '');
insert into "dress_matrix" ("id", "name", "cells_x", "cells_y") values (null, '1', '20', '4,6,8,10,12,14,16');
insert into "dress_matrix" ("id", "name", "cells_x", "cells_y") values (null, '2', '0', 'S (1),M (2),L (3),XL (4),XXL (5),XXXL (6)');
insert into "dress_matrix" ("id", "name", "cells_x", "cells_y") values (null, 'костюм', '164/170,170/176,176,176/182,184,182/188,188/194,Брак (0)', '36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53');
insert into "dress_matrix" ("id", "name", "cells_x", "cells_y") values (null, '3', '0', '104,108,112,116,120,124,128,132,136,140');
insert into "dress_matrix" ("id", "name", "cells_x", "cells_y") values (null, 'сорочка', '164,170,176,182,188,194', '44,46,48,50,52,54,56,58,60,62,64,66,68,70');
