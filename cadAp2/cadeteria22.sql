--
-- File generated with SQLiteStudio v3.3.3 on vi. nov. 11 01:11:08 2022
--
-- Text encoding used: UTF-8
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;
CREATE TABLE cadeteria (id_cadeteria INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, cadeteria VARCHAR (50) NOT NULL);
CREATE TABLE cadete (id_cadete INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, cadete VARCHAR (50) NOT NULL, direccion VARCHAR (50) NOT NULL, telefono VARCHAR (11) NOT NULL, id_cadeteria INTEGER CONSTRAINT fkcada REFERENCES cadeteria (id_cadeteria) ON DELETE SET NULL ON UPDATE CASCADE NOT NULL DEFAULT (1));
CREATE TABLE cliente (id_cliente INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, cliente VARCHAR (50) NOT NULL, direccion VARCHAR (50) NOT NULL, referencia_direccion VARCHAR (50), telefono VARCHAR (11) NOT NULL);
CREATE TABLE pedido (id_pedido INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, detalle VARCHAR (50) NOT NULL, estado VARCHAR (20) NOT NULL CONSTRAINT dfpend DEFAULT Pendiente, id_cliente INTEGER CONSTRAINT fkcli REFERENCES cliente (id_cliente) ON DELETE CASCADE ON UPDATE CASCADE NOT NULL, id_cadete INTEGER CONSTRAINT fkcad REFERENCES cadete (id_cadete) ON DELETE SET NULL ON UPDATE CASCADE);

-- Table: cadete
INSERT INTO cadete (id_cadete, cadete, direccion, telefono, id_cadeteria) VALUES (1, 'Seiya', 'Pegaso 99', '3815555555', 1);
INSERT INTO cadete (id_cadete, cadete, direccion, telefono, id_cadeteria) VALUES (2, 'Shiryu', 'El Dragon 123', '3815611111', 1);
INSERT INTO cadete (id_cadete, cadete, direccion, telefono, id_cadeteria) VALUES (3, 'Hyoga', 'Los Cisnes 88, Siberia', '3815050505', 1);
INSERT INTO cadete (id_cadete, cadete, direccion, telefono, id_cadeteria) VALUES (4, 'Shun', 'Galaxia Andromeda', '3814999999', 1);
INSERT INTO cadete (id_cadete, cadete, direccion, telefono, id_cadeteria) VALUES (5, 'Ikki', 'Pje. Fenix 23 ', '3816666666', 1);

-- Table: cadeteria
INSERT INTO cadeteria (id_cadeteria, cadeteria) VALUES (1, 'SeiYa SM');
INSERT INTO cadeteria (id_cadeteria, cadeteria) VALUES (2, 'SeiYa YB');
INSERT INTO cadeteria (id_cadeteria, cadeteria) VALUES (3, 'SeiYa Tafí');

-- Table: cliente
INSERT INTO cliente (id_cliente, cliente, direccion, referencia_direccion, telefono) VALUES (1, 'Saori Kido', 'Atenas 7501', 'Mansión de rejas negras, toque palmas', '3814010509');
INSERT INTO cliente (id_cliente, cliente, direccion, referencia_direccion, telefono) VALUES (2, 'Hades', 'Inframundo', 'Morir primero, llevar dos monedas para el viaje', '3814567899');
INSERT INTO cliente (id_cliente, cliente, direccion, referencia_direccion, telefono) VALUES (3, 'Patriarca', '12 Casas', 'No hay luz cuidado, viaje interdimensional', '3815000000');

-- Table: pedido
INSERT INTO pedido (id_pedido, detalle, estado, id_cliente, id_cadete) VALUES (1, '2 Hamburguesas de lenteja', 'Pendiente', 1, NULL);
INSERT INTO pedido (id_pedido, detalle, estado, id_cliente, id_cadete) VALUES (2, 'Papas Gratinadas Poseidón', 'Pendiente', 1, NULL);
INSERT INTO pedido (id_pedido, detalle, estado, id_cliente, id_cadete) VALUES (3, 'Pizza 7 porciones ', 'Pendiente', 3, NULL);
INSERT INTO pedido (id_pedido, detalle, estado, id_cliente, id_cadete) VALUES (4, 'Helado frutilla / vainilla / chocolate', 'Pendiente', 2, NULL);
INSERT INTO pedido (id_pedido, detalle, estado, id_cliente, id_cadete) VALUES (5, 'Helado menta granizada c/ kinotos', 'Pendiente', 3, NULL);
INSERT INTO pedido (id_pedido, detalle, estado, id_cliente, id_cadete) VALUES (6, 'Promo Quimes + Vodka + Gin + Absenta', 'Pendiente', 2, NULL);
INSERT INTO pedido (id_pedido, detalle, estado, id_cliente, id_cadete) VALUES (7, 'Promo Tacos + coca zero', 'Pendiente', 3, NULL);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
