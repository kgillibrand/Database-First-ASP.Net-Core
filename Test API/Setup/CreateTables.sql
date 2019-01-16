drop table if exists users cascade;
drop table if exists posts cascade;
drop table if exists comments cascade;

create table users (
	id serial not null,
	name varchar(200) not null,
	dob date not null,
	primary key(id)
);

create table posts (
	id serial not null,
	author_id int not null,
	name varchar(200) not null,
	content text not null,
	created timestamp not null,
	is_public boolean not null,
	primary key(id),
	foreign key(author_id) references users(id) on delete cascade
);
create index posts_author_id on posts(author_id);
create index is_public on posts(is_public);

create table comments (
	id serial not null,
	post_id int not null,
	author_id int not null,
	content text not null,
	created timestamp not null,
	primary key(id),
	foreign key(post_id) references posts(id) on delete cascade,
	foreign key(author_id) references users(id) on delete cascade
);
create index post_id on comments(post_id);
create index comments_author_id on comments(author_id);
