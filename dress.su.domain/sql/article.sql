create table "article"
(
	"id"				integer			not null primary key,
	"modified"			integer			not null default(strftime('%s', 'now')),
	"name"				varchar(255)	not null collate UTF8CI check(TRIM("name") <> ''),
	"matrix_id"			integer			not null references "dress_matrix"("id"),
	"price_of_purchase"	integer			not null check("price_of_purchase" >= 0),
	"price_of_sell"		integer			not null check("price_of_sell" >= 0)
);

create index "ix_article_name" on "article" ("name" collate UTF8CI);
