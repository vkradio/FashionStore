create table "point_of_sale"
(
	"id"	integer			not null primary key,
	"name"	varchar(255)	not null collate UTF8CI unique check(TRIM("name") <> ''),
	"order_number" integer	not null default (0),
	"deleted" integer		not null default (0)
);

-- alter table "point_of_sale"
-- add "order_number" integer not null default (0);

-- update "point_of_sale" set "order_number" = 1 where "id" = 7;
-- update "point_of_sale" set "order_number" = 2 where "id" = 6;
-- update "point_of_sale" set "order_number" = 3 where "id" = 5;
-- update "point_of_sale" set "order_number" = 4 where "id" = 1;
-- update "point_of_sale" set "order_number" = 5 where "id" = 4;
-- update "point_of_sale" set "order_number" = 6 where "id" = 2;