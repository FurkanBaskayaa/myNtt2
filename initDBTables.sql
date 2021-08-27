CREATE TABLE demo.users(
	user_id INTEGER NOT NULL AUTO_INCREMENT,
    username VARCHAR(40) NOT NULL,
    password VARCHAR(40) NOT NULL,	
    mail_address VARCHAR(255) NOT NULL,
	PRIMARY KEY(user_id));
    
CREATE TABLE demo.customer_account(
	user_id INTEGER,
	PRIMARY KEY(user_id),
	FOREIGN KEY(user_id) REFERENCES demo.users(user_id) ON DELETE CASCADE);
    
CREATE TABLE demo.supplier_account(
	user_id INTEGER,
	PRIMARY KEY(user_id),
	FOREIGN KEY(user_id) REFERENCES demo.users(user_id) ON DELETE CASCADE);
    
CREATE TABLE demo.products(
	product_id INTEGER NOT NULL AUTO_INCREMENT,
    photo_url VARCHAR(100),
    rating NUMERIC(3,2),
	PRIMARY KEY(product_id));

CREATE TABLE demo.product_translations(
	product_id INTEGER NOT NULL,
	language VARCHAR(50) NOT NULL,
    product_name VARCHAR(50) NOT NULL,
    price INTEGER NOT NULL,
    CHECK (price > 0),
    currency VARCHAR(30),
    summary VARCHAR(500),
	PRIMARY KEY(product_id, language),
    FOREIGN KEY (product_id) REFERENCES demo.products(product_id) ON DELETE CASCADE);
    
CREATE TABLE demo.catagory(
	catagory_id INTEGER NOT NULL AUTO_INCREMENT,
	PRIMARY KEY(catagory_id));

CREATE TABLE demo.catagory_translations(
	catagory_id INTEGER NOT NULL,
    catagory_name VARCHAR(50) NOT NULL,
    language VARCHAR(50) NOT NULL,
	PRIMARY KEY(catagory_id,language),
    FOREIGN KEY (catagory_id) REFERENCES demo.catagory(catagory_id) ON DELETE CASCADE);
    
CREATE TABLE demo.rates(
	user_id INTEGER,
    product_id INTEGER,
    score INTEGER,
    CHECK(score <= 5),
    CHECK(score >= 0),
	PRIMARY KEY(user_id, product_id),
	FOREIGN KEY(user_id) REFERENCES demo.users(user_id) ON DELETE CASCADE,
    FOREIGN KEY(product_id) REFERENCES demo.products(product_id) ON DELETE CASCADE);

CREATE TABLE demo.supplies(
	user_id INTEGER,
    product_id INTEGER,
	PRIMARY KEY(product_id),
	FOREIGN KEY(user_id) REFERENCES demo.users(user_id) ON DELETE CASCADE,
    FOREIGN KEY(product_id) REFERENCES demo.products(product_id) ON DELETE CASCADE);
    
CREATE TABLE demo.belongs_to(
    product_id INTEGER,
    catagory_id INTEGER,
	PRIMARY KEY(product_id),
	FOREIGN KEY(catagory_id) REFERENCES demo.catagory(catagory_id) ON DELETE CASCADE,
    FOREIGN KEY(product_id) REFERENCES demo.products(product_id) ON DELETE CASCADE);
    