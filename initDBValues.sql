insert into demo.products(rating) values(5);
insert into demo.products(rating) values(4);
insert into demo.products(rating) values(3);
insert into demo.products(rating) values(5);
insert into demo.products(rating) values(4);
insert into demo.products(rating) values(3);
insert into demo.products(rating) values(5);
insert into demo.products(rating) values(4);
insert into demo.products(rating) values(3);

insert into demo.product_translations values(1, "english", "gold sword", 500, "£", "It was the spear of the Elven king Gil-galad. The weapon was used by Gil-galad in his duel with Sauron. It has +50% pierce damage against orcs.");
insert into demo.product_translations values(1, "turkish", "altin kiliç", 5000, "₺", "Kral Gil-galad'in silahidir. Bu silah Gil-galad tarafindan Sauron ile olan düellosunda kullanilmiştir.Orklara karş +%50 delici hasari");
insert into demo.product_translations values(2, "english", "silver sword", 400, "£", "It was one of the two swords forged by Eöl. It has +50% critical damage against goblins");
insert into demo.product_translations values(2, "turkish", "gümüş kiliç", 4000, "₺", "Kara elf Eöl tarafindan dövülen iki kiliçtan biridir. Goblinlere karşi +%50 kritik hasari");
insert into demo.product_translations values(3, "english", "bronze sword", 300, "£", "It was a knife borne by Curiffin. It has +50% melee damage to undeads.");
insert into demo.product_translations values(3, "turkish", "bronz kiliç", 3000, "₺", "Curiffin tarafindan kullanilmiş bir biçaktir. Ölümsüzlere karşi +%50 yakin dövüş hasari");

insert into demo.product_translations values(4, "english", "gold shield", 50, "£", "strongest shield");
insert into demo.product_translations values(4, "turkish", "altin kalkan", 500, "₺", "güçlü kalkan");
insert into demo.product_translations values(5, "english", "silver shield", 40, "£", "medium strength shield");
insert into demo.product_translations values(5, "turkish", "gümüş kalkan", 400, "₺", "orta güçlü kalkan");
insert into demo.product_translations values(6, "english", "bronze shield", 30, "£", "weak shield");
insert into demo.product_translations values(6, "turkish", "bronz kalkan", 300, "₺", "zayif kalkan");

insert into demo.product_translations values(7, "english", "gold armor", 300, "£", "strongest armor");
insert into demo.product_translations values(7, "turkish", "altin zirh", 3000, "₺", "güçlü zirh");
insert into demo.product_translations values(8, "english", "silver armor", 200, "£", "medium strength armor");
insert into demo.product_translations values(8, "turkish", "gümüş zirh", 2000, "₺", "orta güçlü zirh");
insert into demo.product_translations values(9, "english", "bronze armor", 100, "£", "weak armor");
insert into demo.product_translations values(9, "turkish", "bronz zirh", 1000, "₺", "zayif zirh");

insert into demo.catagory()values();
insert into demo.catagory()values();
insert into demo.catagory()values();

insert into demo.catagory_translations values(1,"swords","english");
insert into demo.catagory_translations values(1,"kiliçlar","turkish");
insert into demo.catagory_translations values(2,"shields","english");
insert into demo.catagory_translations values(2,"kalkanlar","turkish");
insert into demo.catagory_translations values(3,"armors","english");
insert into demo.catagory_translations values(3,"zirhlar","turkish");

insert into demo.belongs_to values(1,1);
insert into demo.belongs_to values(2,1);
insert into demo.belongs_to values(3,1);
insert into demo.belongs_to values(4,2);
insert into demo.belongs_to values(5,2);
insert into demo.belongs_to values(6,2);
insert into demo.belongs_to values(7,3);
insert into demo.belongs_to values(8,3);
insert into demo.belongs_to values(9,3);
