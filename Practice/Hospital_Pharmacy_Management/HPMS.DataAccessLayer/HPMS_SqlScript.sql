USE master
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name='PharmacyDB')
DROP DATABASE PharmacyDB
GO

CREATE DATABASE PharmacyDB
GO

USE PharmacyDB
GO

-- Medicines
CREATE TABLE Medicines(
    MedicineID CHAR(4) PRIMARY KEY,
    MedicineName VARCHAR(50) NOT NULL,
    Price MONEY NOT NULL,
    Stock INT NOT NULL
)

INSERT INTO Medicines VALUES
('M101','Paracetamol',10,100),
('M102','Ibuprofen',15,80),
('M103','Amoxicillin',25,50),
('M104','Cough Syrup',50,40)

-- Patients
CREATE TABLE Patients(
    PatientID CHAR(4) PRIMARY KEY,
    PatientName VARCHAR(50),
    Age INT
)

INSERT INTO Patients VALUES
('P101','Rahul',25),
('P102','Anita',30)

-- Prescriptions
CREATE TABLE Prescriptions(
    PrescriptionID INT IDENTITY PRIMARY KEY,
    PatientID CHAR(4) REFERENCES Patients(PatientID),
    DateOfPrescription DATE
)

-- Prescription Details
CREATE TABLE PrescriptionDetails(
    ID INT IDENTITY PRIMARY KEY,
    PrescriptionID INT REFERENCES Prescriptions(PrescriptionID),
    MedicineID CHAR(4) REFERENCES Medicines(MedicineID),
    Quantity INT
)
GO
CREATE PROCEDURE usp_CreatePrescription
(
    @PatientID CHAR(4),
    @Date DATE,
    @PrescriptionID INT OUTPUT
)
AS
BEGIN
    DECLARE @ret INT = 0

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Patients WHERE PatientID=@PatientID)
            RETURN -1

        INSERT INTO Prescriptions VALUES(@PatientID,@Date)

        SET @PrescriptionID = SCOPE_IDENTITY()

        RETURN 1
    END TRY
    BEGIN CATCH
        SET @PrescriptionID = 0
        RETURN -99
    END CATCH
END
GO
CREATE FUNCTION ufn_CalculateBill
(
    @PrescriptionID INT
)
RETURNS MONEY
AS
BEGIN
    DECLARE @total MONEY

    SELECT @total = SUM(m.Price * pd.Quantity)
    FROM PrescriptionDetails pd
    JOIN Medicines m ON pd.MedicineID = m.MedicineID
    WHERE pd.PrescriptionID = @PrescriptionID

    RETURN @total
END
GO
CREATE FUNCTION ufn_GetPrescriptionDetails
(
    @PrescriptionID INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT m.MedicineName, pd.Quantity, m.Price
    FROM PrescriptionDetails pd
    JOIN Medicines m ON pd.MedicineID = m.MedicineID
    WHERE pd.PrescriptionID = @PrescriptionID
)
GO