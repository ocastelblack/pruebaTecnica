 create table clientes (
  idClientes int primary key identity (1,1),
  TipoIentificacion varchar(100),
  numeroIdentificacion int,
  nombres varchar(200),
  apellido varchar(200),
  corre varchar(100),
  fechaNacimiento datetime,
  fechaCreacion datetime,
  fechaModificacion datetime
 )


 create table productos (
  idProductos int primary key identity (1,1),
  tipoCuenta varchar(100),
  numeroCuenta varchar(max);,
  estado varchar(50),
  saldo int,
  exenteGmf varchar(10),
  fechaCreacion datetime,
  fechaModificacion datetime,
  id_Cliente int FOREIGN KEY (id_Cliente) REFERENCES clientes(idClientes)
 )

 create table transacciones(
 idTransacciones int primary key identity (1,1),
 tipoTransaccion varchar(100),
 descripcionTransaccion varchar(max),
 id_Cliente int FOREIGN KEY (id_Cliente) REFERENCES clientes(idClientes),
 id_Producto int FOREIGN KEY (id_Producto) REFERENCES productos(idProductos)
 )