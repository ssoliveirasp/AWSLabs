import json
import boto3
import uuid

TABLE_NAME = 'replace-me'

def get_priority(message):
    if 'MessageAttributes' not in message:
        return ''

    if 'priority' not in message['MessageAttributes']:
        return ''

    if message['MessageAttributes']['priority']['Type'] == 'String':
        return message['MessageAttributes']['priority']['Value']

    return ''

def process_message(message):
    print(message)

    item_id = str(uuid.uuid4())
    body = json.loads(message['body'])
    priority = get_priority(body)

    item = {
        'Id': {
            'S': item_id
        },
        'Priority': {
            'S': priority
        },
        'Contents': {
            'S': body['Message']
        }
    }

    dynamodb = boto3.client('dynamodb')
    response = dynamodb.put_item(TableName=TABLE_NAME, Item=item)
    status = response['ResponseMetadata']['HTTPStatusCode']

def lambda_handler(event, context):
    print(event)

    for record in event['Records']:
        process_message(record)
