DROP TABLE IF EXISTS employee;
DROP TABLE IF EXISTS department;

CREATE TABLE department
(
    id   int primary key,
    name varchar(100)
);

CREATE TABLE employee
(
    id            int primary key,
    department_id int,
    chief_id      int,
    name          varchar(250),
    salary        float,
    FOREIGN KEY (department_id) REFERENCES department (id),
    FOREIGN KEY (chief_id) REFERENCES employee (id)
);


-- test departments
INSERT INTO department(id, name) VALUES (1, 'digital-design');
INSERT INTO department(id, name) VALUES (2, 'good');

-- test chiefs
INSERT INTO employee(id, department_id, name, salary)
VALUES (1, 1, 'digital-chief', 100000.00);
INSERT INTO employee(id, department_id, name, salary)
VALUES (2, 2, 'good-chief', 95000.00);
-- test digital employees
INSERT INTO employee(id, department_id, chief_id, name, salary)
VALUES (3, 1, 1, 'digital-hr', 50000.00);
INSERT INTO employee(id, department_id, chief_id, name, salary)
VALUES (4, 1, 1, 'digital-best-programmer', 90000.00);
INSERT INTO employee(id, department_id, chief_id, name, salary)
VALUES (5, 1, 1, 'digital-best-devops', 80000.00);
-- test good workers
INSERT INTO employee(id, department_id, chief_id, name, salary)
VALUES (6, 2, 2, 'good-programmer', 80000.00);
INSERT INTO employee(id, department_id, chief_id, name, salary)
VALUES (7, 2, 2, 'good-devops', 70000.00);
-- test employees for the last task
INSERT INTO employee(id, name, salary)
VALUES (8, 'Рн', 6000.00);
INSERT INTO employee(id, name, salary)
VALUES (9, 'Рwwwн', 6000.00);
INSERT INTO employee(id, name, salary)
VALUES (10, 'Р', 1000.00);


-- 1. Сотрудник с максимальной зарплатой
SELECT name, salary
FROM employee
ORDER BY salary DESC
LIMIT 1;

-- 2. Отдел с самой высокой зарплатой между сотрудниками
SELECT d.name, MAX(salary) as max_salary
FROM department as d
         INNER JOIN employee e on d.id = e.department_id
GROUP BY d.name
ORDER BY max_salary DESC
LIMIT 1;


-- 3. Отдел с максимальной суммарной зарплатой
SELECT d.name, SUM(salary) as summary_salary
FROM department as d
         INNER JOIN employee e on d.id = e.department_id
GROUP BY d.name
ORDER BY summary_salary DESC
LIMIT 1;


-- 4. Сотрудник, чьё имя начинается на "P" и заканчивается "н"
SELECT id, name
FROM employee
WHERE name LIKE 'Р%н';