CREATE TABLE steel_grades (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE orders (
    id SERIAL PRIMARY KEY,
    order_number VARCHAR(50) NOT NULL UNIQUE,
    manufacturer VARCHAR(50) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    status VARCHAR(20) CHECK (status IN ('новый', 'в работе', 'выполнен')) NOT NULL
);

CREATE TABLE positions (
    id SERIAL PRIMARY KEY,
    order_id INT NOT NULL REFERENCES orders(id) ON DELETE CASCADE,
    position_number INT NOT NULL,
    steel_grade_id INT REFERENCES steel_grades(id),
    diameter NUMERIC(10, 2) NOT NULL,
    wall_thickness NUMERIC(10, 2) NOT NULL,
    volume NUMERIC(10, 2) NOT NULL,
    unit VARCHAR(20) NOT NULL,
    status VARCHAR(20) CHECK (status IN ('новая', 'в работе', 'выполнена')) NOT NULL
);

CREATE UNIQUE INDEX idx_order_position_unique ON positions(order_id, position_number);
