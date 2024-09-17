-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 02, 2024 at 03:32 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sys_emp`
--

-- --------------------------------------------------------

--
-- Table structure for table `db_dayf`
--

CREATE TABLE `db_dayf` (
  `id_dayf` int(10) NOT NULL,
  `date` varchar(20) NOT NULL,
  `description` varchar(100) NOT NULL,
  `id_emp` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `db_depart`
--

CREATE TABLE `db_depart` (
  `id_dep` int(20) NOT NULL,
  `dep_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `db_depart`
--

INSERT INTO `db_depart` (`id_dep`, `dep_name`) VALUES
(4, 'ບໍລິຫານ'),
(5, 'ບັນຊີ-ການເງິນ'),
(8, 'ຄຸ້ມຄອງ');

-- --------------------------------------------------------

--
-- Table structure for table `db_edu`
--

CREATE TABLE `db_edu` (
  `id_branc` int(20) NOT NULL,
  `branc` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `db_edu`
--

INSERT INTO `db_edu` (`id_branc`, `branc`) VALUES
(2, 'ສາຂາແຂວງຫົວພັນ'),
(3, 'ສາຂາໃຫຍ່ ນະຄອນຫຼວງ');

-- --------------------------------------------------------

--
-- Table structure for table `db_emp`
--

CREATE TABLE `db_emp` (
  `id_emp` int(20) NOT NULL,
  `emp_name` varchar(50) NOT NULL,
  `emp_gender` varchar(20) NOT NULL,
  `emp_salary` varchar(20) NOT NULL,
  `id_pos` int(20) NOT NULL,
  `emp_age` int(10) NOT NULL,
  `emp_tel` varchar(10) NOT NULL,
  `emp_email` varchar(20) NOT NULL,
  `id_dep` int(20) NOT NULL,
  `id_branc` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `db_emp`
--

INSERT INTO `db_emp` (`id_emp`, `emp_name`, `emp_gender`, `emp_salary`, `id_pos`, `emp_age`, `emp_tel`, `emp_email`, `id_dep`, `id_branc`) VALUES
(2, 'sinxai', 'ຊາຍ', '12,000,000', 6, 23, '22111234', ' si122@gmail.com', 4, 3);

-- --------------------------------------------------------

--
-- Table structure for table `db_posit`
--

CREATE TABLE `db_posit` (
  `id_pos` int(20) NOT NULL,
  `pos` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `db_posit`
--

INSERT INTO `db_posit` (`id_pos`, `pos`) VALUES
(1, 'ບໍລິຫານ'),
(5, 'ພະນັກງານທົ່ວໄປ'),
(6, 'HR');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `db_dayf`
--
ALTER TABLE `db_dayf`
  ADD PRIMARY KEY (`id_dayf`),
  ADD KEY `id_emp` (`id_emp`);

--
-- Indexes for table `db_depart`
--
ALTER TABLE `db_depart`
  ADD PRIMARY KEY (`id_dep`);

--
-- Indexes for table `db_edu`
--
ALTER TABLE `db_edu`
  ADD PRIMARY KEY (`id_branc`);

--
-- Indexes for table `db_emp`
--
ALTER TABLE `db_emp`
  ADD PRIMARY KEY (`id_emp`),
  ADD KEY `id_pos` (`id_pos`);

--
-- Indexes for table `db_posit`
--
ALTER TABLE `db_posit`
  ADD PRIMARY KEY (`id_pos`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `db_dayf`
--
ALTER TABLE `db_dayf`
  MODIFY `id_dayf` int(10) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `db_depart`
--
ALTER TABLE `db_depart`
  MODIFY `id_dep` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `db_edu`
--
ALTER TABLE `db_edu`
  MODIFY `id_branc` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `db_emp`
--
ALTER TABLE `db_emp`
  MODIFY `id_emp` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `db_posit`
--
ALTER TABLE `db_posit`
  MODIFY `id_pos` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
