1. In your Session Manager shell session, enter the following command to change to the default Amazon Linux user (ec2-user) running in a bash shell:

sudo -i -u ec2-user


sudo yum -y install mysql

mysql -h <your.endpoint> -u cloudacademy -p rdsappdb

CREATE TABLE customer ( id INT, name VARCHAR(100) );

DESC customer;
