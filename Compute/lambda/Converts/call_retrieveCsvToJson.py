import boto3
s3 = boto3.client('s3')

def lambda_handler(event, context):

    response = s3.get_object(
      Bucket='arn:aws:s3-object-lambda:us-west-2:853018179017:accesspoint/lab-olap',
      Key= 'dev-team.csv'
    )

    return response['Body'].read()
