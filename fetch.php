<?php

	//192.168.2.16/fetch.php?uid= &bal= &in=

foreach($_REQUEST as $key => $value)
{
	if($key == "uid"){
		$bufferUid = $value;
	}

	if($key == "bal"){
		$bufferBal = $value;
	}

	if($key == "in"){
		$bufferIn = $value;
	}
}

$con=mysqli_connect("localhost","root","Welkom01!", "Frowrail_Userdata");


if (mysqli_connect_errno()) {
  echo "Failed to connect to MySQL: " . mysqli_connect_error();
}

$failed = false;

if ($bal != -404) {
		mysqli_query($con,"UPDATE Userdata SET BALANCE = $bufferBal WHERE UID = $bufferUid");
			if (mysql_affected_rows() == 0) {
				$failed = true;
			}
}

if ($in != -404) {
		mysqli_query($con,"UPDATE Userdata SET CHECKIN = $bufferIn WHERE UID = $bufferUid");
			if (mysql_affected_rows() == 0) {
				$failed = true;
			}
}

if ($failed == true) {
	if ($bal == -404) {
		$bal = 0;
	}

	mysql_query($con,"INSERT INTO Userdata ($bufferUid, $bufferBal, $bufferIn)")
}

$result = mysqli_query($con,"SELECT * FROM Userdata");

while($row = mysqli_fetch_array($result)) {
if($row['UID'] == $uid){
	$returnValue1 = $row['BALANCE'];
	$returnValue1 = $row['CHECKIN'];
	}
}

$returnPayload = strval(strlen($returnValue1)) . "_" . strval($returnValue1) . "#" . strval($returnValue2)
$returnPayload = "**TByte*" . "##" . $returnPayload; 

#	**TByte*##3_bal#1


?>