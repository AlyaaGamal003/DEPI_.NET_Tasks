# ERD

![[Pasted image 20250703180440.png]]

# Mapping
## **1. Employee**

| Column        | Data Type   | Constraints                         |
| ------------- | ----------- | ----------------------------------- |
| SSN           | INT         | **PK**, NOT NULL, UNIQUE            |
| F_name        | VARCHAR(50) | NOT NULL                            |
| L_name        | VARCHAR(50) | NOT NULL                            |
| Gender        | CHAR(1)     | NOT NULL, CHECK (IN 'M', 'F')       |
| Birth_Date    | DATE        | NOT NULL                            |
| Dnum          | INT         | **FK → Department(Dnum)**, NOT NULL |
| SupervisorSSN | INT         | FK → Employee(SSN), NULLABLE        |

---
## **2. Department**

|Column|Data Type|Constraints|
|---|---|---|
|Dnum|INT|**PK**, NOT NULL|
|Dname|VARCHAR(50)|NOT NULL, UNIQUE|

---

## **3. Department_Locations**

| Column   | Data Type    | Constraints                 |
| -------- | ------------ | --------------------------- |
| Dnum     | INT          | **PK** → Department(Dnum)** |
| Location | VARCHAR(100) | **PK**, NOT NULL            |

---

## **4. Project**

|Column|Data Type|Constraints|
|---|---|---|
|PNumber|INT|**PK**, NOT NULL|
|Pname|VARCHAR(100)|NOT NULL|
|City|VARCHAR(100)|NOT NULL|
|Dnum|INT|**FK → Department(Dnum)**, NOT NULL|

---
## **5. Dependent**

| Column         | Data Type   | Constraints                    |
| -------------- | ----------- | ------------------------------ |
| SSN            | INT         | **PK**, **FK → Employee(SSN)** |
| Dependent_Name | VARCHAR(50) | **PK** (with SSN), NOT NULL    |
| Gender         | CHAR(1)     | NOT NULL, CHECK (IN 'M', 'F')  |
| Birth_Date     | DATE        | NOT NULL                       |

---
## **6. Works_On**

| Column  | Data Type    | Constraints                       |
| ------- | ------------ | --------------------------------- |
| SSN     | INT          | **PK**, **FK → Employee(SSN)**    |
| PNumber | INT          | **PK**, **FK → Project(PNumber)** |
| Hours   | DECIMAL(4,1) | NOT NULL, CHECK (Hours >= 0)      |

---
## **7. Manages**

| Column    | Data Type | Constraints                       |
| --------- | --------- | --------------------------------- |
| Dnum      | INT       | **PK**, **FK → Department(Dnum)** |
| SSN       | INT       | NOT NULL, **FK → Employee(SSN)**  |
| Hire_Date | DATE      | NOT NULL                          |

---
## **SQL MAPPING FOR TASK** 

![[Pasted image 20250703190601.png]]