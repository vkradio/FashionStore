create table "doc_sale"(	"id"					integer			not null primary key references "doc"("id"),	"time_sold"				integer			not null,	"art_id"				integer			null references "article"("id"),	"art_name"				varchar(255)	not null collate UTF8CI,	"art_price_of_purchase"	integer			not null,	"art_price_of_sell"		integer			not null,	"point_of_sale_id"		integer			not null references "point_of_sale"("id"),	"point_of_sale_name"	varchar(255)	not null collate UTF8CI,	"unit_price"			integer			not null check ("unit_price" >= 0),	"unit_count"			integer			not null check ("unit_count" > 0),	"cell_x"				varchar(10)		not null collate UTF8CI,	"cell_y"				varchar(10)		not null collate UTF8CI,	"payment_by_card"		integer			not null check ("payment_by_card" in (0, 1)));