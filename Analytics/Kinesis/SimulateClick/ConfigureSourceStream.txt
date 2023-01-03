CREATE OR REPLACE STREAM "INTERMEDIATE_SQL_STREAM"
(
    "event_timestamp" TIMESTAMP,
    "user_id" VARCHAR(7),
    "device_type" VARCHAR(10),
    "session_timestamp" TIMESTAMP
);
CREATE OR REPLACE  PUMP "STREAM_PUMP1" AS INSERT INTO "INTERMEDIATE_SQL_STREAM"
SELECT  STREAM
    TO_TIMESTAMP("event_timestamp") as "event_timestamp",
    "user_id",
    "device_type",
    CASE WHEN ("event_timestamp" - lag("event_timestamp", 1) OVER (PARTITION BY "user_id" ROWS 1 PRECEDING)) > (10 * 1000) THEN 
            TO_TIMESTAMP("event_timestamp")
         WHEN ("event_timestamp" - lag("event_timestamp", 1) OVER (PARTITION BY "user_id" ROWS 1 PRECEDING)) IS NULL THEN 
            TO_TIMESTAMP("event_timestamp")
         ELSE NULL 
    END AS "session_timestamp"
FROM "SOURCE_SQL_STREAM_001";
