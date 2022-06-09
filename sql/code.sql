create table if not exists UserInfo (
    id int not null primary key,
    login varchar(30) not null,
    hash varchar(30) not null,
    permission int not null
);
\COPY UserInfo FROM 'D:/Desktop/BMSTU/7sem/Web/sql/UserInfo.csv' DELIMITER ',' csv;
--\COPY UserInfo FROM 'D:/Desktop/BMSTU/7sem/WEb/Web/sql/UserInfo.csv' DELIMITER ',' csv;

create table if not exists Management (
    ManagementID int not null primary key,
    AnalysistID int references UserInfo(id),
    ManagerID int references UserInfo(id)
);
\COPY Management FROM 'D:/Desktop/BMSTU/7sem/Web/sql/Management.csv' DELIMITER ',' csv;
--\COPY Management FROM 'D:/Desktop/BMSTU/7sem/WEb/Web/sql/Management.csv' DELIMITER ',' csv;

create table if not exists TeamStatistics (
    StatisticsID int not null primary key,
    numberOfMatchesPlayed int not null,
    league int not null,
    placeInTheLeague int not null,
    numberOfTrophies int not null
);
\COPY TeamStatistics FROM 'D:/Desktop/BMSTU/7sem/Web/sql/TeamStatistics.csv' DELIMITER ',' csv;
--\COPY TeamStatistics FROM 'D:/Desktop/BMSTU/7sem/WEb/Web/sql/TeamStatistics.csv' DELIMITER ',' csv;


create table if not exists Team (
    TeamID int not null primary key,
    ManagementID int references Management(ManagementID),
    StatisticsID int references TeamStatistics(StatisticsID),
    Name varchar(30) not null,
    HeadCoach varchar(30) not null,
    Country varchar(30) not null,
    Stadium varchar(60) not null,
    Balance int not null
);
\COPY Team FROM 'D:/Desktop/BMSTU/7sem/Web/sql/Team.csv' DELIMITER ',' csv;
--\COPY Team FROM 'D:/Desktop/BMSTU/7sem/WEb/Web/sql/Team.csv' DELIMITER ',' csv;

create table if not exists PlayerStatistics (
    StatisticsID int not null primary key,
    numberOfWashers int not null,
    averageGameTime int not null
);
\COPY PlayerStatistics FROM 'D:/Desktop/BMSTU/7sem/Web/sql/Statistics.csv' DELIMITER ',' csv;
--\COPY PlayerStatistics FROM 'D:/Desktop/BMSTU/7sem/WEb/Web/sql/Statistics.csv' DELIMITER ',' csv;

create table if not exists PlayerSpecifications (
    SpecificationsID int not null primary key,
    shooting int not null,
    defense int not null,
    skating int not null,
    physical int not null
);
\COPY PlayerSpecifications FROM 'D:/Desktop/BMSTU/7sem/Web/sql/PlayerSpecifications.csv' DELIMITER ',' csv;
--\COPY PlayerSpecifications FROM 'D:/Desktop/BMSTU/7sem/WEb/Web/sql/PlayerSpecifications.csv' DELIMITER ',' csv;

create table if not exists Player (
    PlayerID int not null primary key,
    TeamID int references Team(TeamID),
    PlayerStatistics int references PlayerStatistics(StatisticsID),
    PlayerSpecifications int references PlayerSpecifications(SpecificationsID),
    name varchar(30) not null,
    position varchar(10) not null,
    weight int not null,
    height int not null,
    number int not null,
    age int not null,
    country varchar(50) not null,
    cost int not null
);
\COPY Player FROM 'D:/Desktop/BMSTU/7sem/Web/sql/Player.csv' DELIMITER ',' csv;
--\COPY Player FROM 'D:/Desktop/BMSTU/7sem/WEb/Web/sql/Player.csv' DELIMITER ',' csv;

create table if not exists AvailableDeals (
    Id int primary key not null,
    PlayerID int references Player(PlayerID),
    ToManagementID int references Management(ManagementID),
    FromManagementID int references Management(ManagementID),
    cost int not null,
    status int not null
);

create table if not exists DesiredPlayers (
    Id int primary key not null,
    PlayerID int references Player(PlayerID),
    ManagementID int references Management(ManagementID)
); 

-- Password encryption
create or replace procedure password_encryption(new_password varchar(30), UserId int)
language plpgsql
as $$
begin 
	update UserInfo
	set hash = new_password
	where id = UserId;
end
$$;

insert into UserInfo values (1, '567', '567', 1);
insert into UserInfo values (2, '567', '567', 2);
insert into Management values (1, 1, 2);
insert into Team values (1, 1, 'Dynamo', 'Denis Sklif', 'Russia', 'VTB', 5000);
insert into Statistics values (1, 30, 25);
insert into Statistics values (2, 20, 15);
insert into Player values (1, 1, 1, 'Vlad', 'ca', 75, 180, 17, 27, 'Russia', 500);
insert into Player values (2, 1, 2, 'Denis', 'ca', 93, 193, 21, 20, 'Russia', 500);

-- ROLE Guest
create role guest with login password '1234';
grant select on table userinfo to guest;
-- ROLE Analytic
create role analytic with login password '1234';
grant select on table player, team, management, PlayerStatistics, TeamStatistics, PlayerSpecifications to analytic;
grant all privileges on table desiredplayers to analytic;
-- ROLE Manager
create role manager with login password '1234';
grant select on table player, team, management, PlayerStatistics, TeamStatistics, PlayerSpecifications to manager;
grant select, delete on table desiredplayers to manager;
grant select, delete, insert on table availabledeals to manager;

select PlayerTeam.playerid as PlayerID, PlayerTeam.player as Player, PlayerTeam.Team, statistics.numberOfWashers as Washers, statistics.averageGameTime as gametime
from (
    select PlayerID as playerid, player.Name as player, player.Statistics as PlayerStat, team.Name as Team
    from player join team on player.TeamID = team.TeamID
) as PlayerTeam
join statistics on PlayerTeam.PlayerStat = statistics.StatisticsID;


drop function if exists GetPlayers;
create function GetPlayers()
returns table
(
    PlayerID int,
    Player varchar(40),
    Team varchar(40),
    Washers int,
    gametime int
)
language sql
as $$
    select PlayerTeam.playerid as PlayerID, PlayerTeam.player as Player, PlayerTeam.Team as Team, PlayerStatistics.numberOfWashers as Washers, PlayerStatistics.averageGameTime as gametime
    from (
        select PlayerID as playerid, player.Name as player, player.PlayerStatistics as PlayerStat, team.Name as Team
        from player join team on player.TeamID = team.TeamID
    ) as PlayerTeam
    join PlayerStatistics on PlayerTeam.PlayerStat = PlayerStatistics.StatisticsID;
$$;

drop procedure if exists makedeal(int, int, int, int, int);
create or replace procedure makedeal(LastTeamID int, NewTeamID int, LastTeamBalance int, NewTeamBalance int, plID int)
    language plpgsql
as
$$
begin
    update team
    set Balance = LastTeamBalance
    where TeamID = LastTeamID;

    update team
    set Balance = NewTeamBalance
    where TeamID = NewTeamID;

    update player
    set TeamID = NewTeamID
    where player.playerid = plID;
end
$$;