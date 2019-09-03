<?php

$pdo = new PDO('mysql:host=localhost;dbname=unilabsql1', "unilabsql1", "#1jYW5§p", array(PDO::MYSQL_ATTR_INIT_COMMAND => 'SET NAMES utf8'));

$pdo->exec("INSERT INTO `scores`(`name`, `time`) VALUES (\"" . $_GET['n'] . "\", " . $_GET['t'] . ")");
