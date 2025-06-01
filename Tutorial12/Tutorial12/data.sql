-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2021-04-20 04:46:38.52

-- tables
-- Table: Client
CREATE TABLE Client (
                        IdClient int  NOT NULL IDENTITY,
                        FirstName nvarchar(120)  NOT NULL,
                        LastName nvarchar(120)  NOT NULL,
                        Email nvarchar(120)  NOT NULL,
                        Telephone nvarchar(120)  NOT NULL,
                        Pesel nvarchar(120)  NOT NULL,
                        CONSTRAINT Client_pk PRIMARY KEY  (IdClient)
);

-- Table: Client_Trip
CREATE TABLE Client_Trip (
                             IdClient int  NOT NULL,
                             IdTrip int  NOT NULL,
                             RegisteredAt datetime  NOT NULL,
                             PaymentDate datetime  NULL,
                             CONSTRAINT Client_Trip_pk PRIMARY KEY  (IdClient,IdTrip)
);

-- Table: Country
CREATE TABLE Country (
                         IdCountry int  NOT NULL IDENTITY,
                         Name nvarchar(120)  NOT NULL,
                         CONSTRAINT Country_pk PRIMARY KEY  (IdCountry)
);

-- Table: Country_Trip
CREATE TABLE Country_Trip (
                              IdCountry int  NOT NULL,
                              IdTrip int  NOT NULL,
                              CONSTRAINT Country_Trip_pk PRIMARY KEY  (IdCountry,IdTrip)
);

-- Table: Trip
CREATE TABLE Trip (
                      IdTrip int  NOT NULL IDENTITY,
                      Name nvarchar(120)  NOT NULL,
                      Description nvarchar(220)  NOT NULL,
                      DateFrom datetime  NOT NULL,
                      DateTo datetime  NOT NULL,
                      MaxPeople int  NOT NULL,
                      CONSTRAINT Trip_pk PRIMARY KEY  (IdTrip)
);

-- foreign keys
-- Reference: Country_Trip_Country (table: Country_Trip)
ALTER TABLE Country_Trip ADD CONSTRAINT Country_Trip_Country
    FOREIGN KEY (IdCountry)
        REFERENCES Country (IdCountry);

-- Reference: Country_Trip_Trip (table: Country_Trip)
ALTER TABLE Country_Trip ADD CONSTRAINT Country_Trip_Trip
    FOREIGN KEY (IdTrip)
        REFERENCES Trip (IdTrip);

-- Reference: Table_5_Client (table: Client_Trip)
ALTER TABLE Client_Trip ADD CONSTRAINT Table_5_Client
    FOREIGN KEY (IdClient)
        REFERENCES Client (IdClient);

-- Reference: Table_5_Trip (table: Client_Trip)
ALTER TABLE Client_Trip ADD CONSTRAINT Table_5_Trip
    FOREIGN KEY (IdTrip)
        REFERENCES Trip (IdTrip);


INSERT INTO Client (FirstName, LastName, Email, Telephone, Pesel) VALUES
                                                                      ('John', 'Doe', 'john.doe@example.com', '123456789', '12345678901'),
                                                                      ('Jane', 'Smith', 'jane.smith@example.com', '987654321', '98765432109'),
                                                                      ('Alice', 'Johnson', 'alice.johnson@example.com', '555555555', '55555555555');
INSERT INTO Country (Name) VALUES
                               ('Poland'),
                               ('Germany'),
                               ('France');
INSERT INTO Trip (Name, Description, DateFrom, DateTo, MaxPeople) VALUES
                                                                      ('Trip to Warsaw', 'Visiting beautiful Warsaw', '2026-06-10', '2026-06-15', 20),
                                                                      ('Trip to Berlin', 'Exploring historical Berlin', '2026-07-01', '2026-07-05', 15),
                                                                      ('Trip to Paris', 'Romantic getaway in Paris', '2024-08-12', '2024-08-18', 25);
INSERT INTO Country_Trip (IdCountry, IdTrip) VALUES
                                                 (1, 1),
                                                 (2, 2),
                                                 (3, 3),
                                                 (1, 2),
                                                 (2, 3);
INSERT INTO Client_Trip (IdClient, IdTrip, RegisteredAt, PaymentDate) VALUES
                                                                          (1, 1, GETDATE(), GETDATE()),
                                                                          (2, 2, GETDATE(), NULL),
                                                                          (3, 3, GETDATE(), GETDATE());

