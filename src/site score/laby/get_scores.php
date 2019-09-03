<?php

$pdo = new PDO('mysql:host=localhost;dbname=unilabsql1', "unilabsql1", "#1jYW5§p", array(PDO::MYSQL_ATTR_INIT_COMMAND => 'SET NAMES utf8'));

$scores = $pdo->query("SELECT * FROM `scores` ORDER BY time LIMIT 10")->fetchAll(PDO::FETCH_ASSOC);
foreach ($scores as $s) {

    $input = $s['time'];

    $uSec = $input % 1000;
    $input = floor($input / 1000);

    $seconds = $input % 60;
    $input = floor($input / 60);

    $minutes = $input % 60;
    $input = floor($input / 60);


    if (isset($_GET['s'])) {
        if($_GET['s']=='n'){
            echo $s['name'] . "<br>";
        }else{
            echo $minutes . ":" . $seconds . "." . $uSec . "<br>";
        }
    } else {
        echo $s['name'] . "\t\t" . $minutes . ":" . $seconds . "." . $uSec . "<br>";
    }
}