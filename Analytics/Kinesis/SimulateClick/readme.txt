### Create Files ###

echo '{
  "user_id": "$USER_ID",
  "event_timestamp": "$EVENT_TIMESTAMP",
  "event_name": "$EVENT_NAME",
  "event_type": "click",
  "device_type": "desktop"
}' > click.json


###  Kinesis and simulate a clickstream ###

USER_IDS=(user1 user2 user3)
EVENTS=(checkout search category detail navigate)
for i in $(seq 1 3000); do
    echo "Iteration: ${i}"
    export USER_ID="${USER_IDS[RANDOM%${#USER_IDS[@]}]}";
    export EVENT_NAME="${EVENTS[RANDOM%${#EVENTS[@]}]}";
    export EVENT_TIMESTAMP=$(($(date +%s) * 1000))
    JSON=$(cat click.json | envsubst)
    echo $JSON
    aws kinesis put-record --stream-name lab-stream --data "${JSON}" --partition-key 1 --region us-west-2
    session_interval=15
    click_interval=2
    if ! (($i%60)); then
        echo "Sleeping for ${session_interval} seconds" && sleep ${session_interval}
    else
        echo "Sleeping for ${click_interval} second(s)" && sleep ${click_interval}
    fi
done
