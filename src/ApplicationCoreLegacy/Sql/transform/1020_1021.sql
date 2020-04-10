alter table "point_of_sale" add "deleted" integer not null default (0);

update "point_of_sale"
set "deleted" = 1
where "id" in (3, 7);
