-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Nov 09, 2017 at 11:20 PM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `band_tracker_test`
--
CREATE DATABASE IF NOT EXISTS `band_tracker_test` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `band_tracker_test`;

-- --------------------------------------------------------

--
-- Table structure for table `bands`
--

CREATE TABLE `bands` (
  `id` int(11) NOT NULL,
  `band_name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bands`
--

INSERT INTO `bands` (`id`, `band_name`) VALUES
(169, 'Jimmy and the Spiders'),
(170, 'Donny Lewis and the Fake News'),
(171, 'Bobby Lewis and the Other News');

-- --------------------------------------------------------

--
-- Table structure for table `bands_venues`
--

CREATE TABLE `bands_venues` (
  `id` int(11) NOT NULL,
  `band_id` int(11) NOT NULL,
  `venue_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

--
-- Dumping data for table `bands_venues`
--

INSERT INTO `bands_venues` (`id`, `band_id`, `venue_id`) VALUES
(7, 39, 75),
(8, 40, 75),
(10, 44, 81),
(11, 45, 81),
(12, 46, 82),
(14, 52, 89),
(15, 53, 89),
(16, 54, 90),
(18, 60, 97),
(19, 61, 97),
(20, 62, 98),
(21, 66, 99),
(23, 68, 105),
(24, 69, 105),
(25, 70, 106),
(26, 74, 107),
(28, 76, 113),
(29, 77, 113),
(30, 78, 114),
(31, 82, 115),
(33, 84, 121),
(34, 85, 121),
(35, 86, 122),
(36, 90, 123),
(38, 92, 129),
(39, 93, 129),
(40, 94, 130),
(41, 98, 131),
(43, 100, 137),
(44, 101, 137),
(45, 102, 138),
(46, 109, 143),
(48, 111, 149),
(49, 112, 149),
(50, 113, 150),
(51, 117, 151),
(53, 119, 157),
(54, 120, 157),
(55, 121, 158),
(56, 123, 160),
(57, 124, 160),
(58, 128, 163),
(60, 130, 169),
(61, 131, 169),
(62, 132, 170),
(63, 136, 171),
(65, 138, 177),
(66, 139, 177),
(67, 140, 178),
(68, 144, 179),
(70, 146, 185),
(71, 147, 185),
(72, 148, 186),
(73, 150, 187),
(74, 153, 189),
(75, 154, 190),
(77, 156, 197),
(78, 157, 197),
(79, 158, 198),
(80, 162, 199),
(81, 163, 200),
(83, 165, 207),
(84, 166, 207),
(85, 167, 208),
(86, 171, 212),
(87, 171, 214);

-- --------------------------------------------------------

--
-- Table structure for table `venues`
--

CREATE TABLE `venues` (
  `id` int(11) NOT NULL,
  `venue_name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `venues`
--

INSERT INTO `venues` (`id`, `venue_name`) VALUES
(212, 'The Hole'),
(214, 'The Walk');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `bands`
--
ALTER TABLE `bands`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `bands_venues`
--
ALTER TABLE `bands_venues`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `venues`
--
ALTER TABLE `venues`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `bands`
--
ALTER TABLE `bands`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=172;
--
-- AUTO_INCREMENT for table `bands_venues`
--
ALTER TABLE `bands_venues`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=88;
--
-- AUTO_INCREMENT for table `venues`
--
ALTER TABLE `venues`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=215;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
