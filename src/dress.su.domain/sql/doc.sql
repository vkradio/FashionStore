create table "doc"
(
	"id"				integer not null primary key,
	"type"				integer not null,
	"time_created"		integer not null default(strftime('%s', 'now')),
	"time_cancelled"	integer null
);
