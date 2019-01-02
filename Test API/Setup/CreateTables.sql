create table posts (
	id serial not null,
	author_id int not null,
	name varchar(200) not null,
	content text not null,
	created timestamp not null,
	is_public boolean not null,
	primary key(id)
);

create table comments (
	id serial not null,
	post_id int not null,
	author_id int not null,
	content text not null,
	created timestamp not null,
	primary key(id),
	foreign key(post_id) references posts(id),
	foreign key(author_id) references users(id)
);

create table users (
	id serial not null,
	name varchar(200) not null,
	dob date not null,
	primary key(id)
);