![[Pasted image 20250709012439.png]]

### 🧍‍♂️ **Employee**

|Column|Data Type|Constraints|
|---|---|---|
|EmpID|INT|**PK**, IDENTITY, NOT NULL, UNIQUE|
|FName|VARCHAR(50)|NOT NULL|
|LName|VARCHAR(50)|NOT NULL|
|Gender|CHAR(1)|NOT NULL, CHECK (Gender IN ('M', 'F'))|
|Salary|DECIMAL(10,2)|DEFAULT 3000, CHECK (Salary >= 0)|
|DeptID|INT|**FK → Department(DeptID)**, NOT NULL|

---

### 🏢 **Department**

| Column    | Data Type    | Constraints                                    |
| --------- | ------------ | ---------------------------------------------- |
| EmpID     | INT          | **PK**, IDENTITY, NOT NULL, UNIQUE             |
| DeptName  | VARCHAR(100) | NOT NULL, UNIQUE                               |
| ManagerID | INT          | **FK → Employee(EmpID)**, UNIQUE, NULL allowed |

---

### 🧒 **Dependent**

| Column   | Data Type   | Constraints                                              |
| -------- | ----------- | -------------------------------------------------------- |
| Name     | VARCHAR(50) | **PK** NOT NULL                                          |
| Gender   | CHAR(1)     | NOT NULL, CHECK (Gender IN ('M', 'F'))                   |
| Relation | VARCHAR(50) | NULL                                                     |
| EmpID    | INT         | **PK,FK → Employee(EmpID)**, NOT NULL, ON DELETE CASCADE |

---

### 📁 **Project**

|Column|Data Type|Constraints|
|---|---|---|
|ProjID|INT|**PK**, IDENTITY, NOT NULL, UNIQUE|
|ProjName|VARCHAR(100)|NOT NULL|
|DeptID|INT|**FK → Department(DeptID)**, NOT NULL|

---

### 🔄 **Employee_Project** (M:N Relationship)

| Column      | Data Type    | Constraints                                         |
| ----------- | ------------ | --------------------------------------------------- |
| EmpID       | INT          | **PK**, **FK → Employee(EmpID)**, ON DELETE CASCADE |
| ProjID      | INT          | **PK**, **FK → Project(ProjID)**, ON DELETE CASCADE |
| HoursWorked | DECIMAL(5,2) | DEFAULT 0, CHECK (HoursWorked >= 0)                 |

