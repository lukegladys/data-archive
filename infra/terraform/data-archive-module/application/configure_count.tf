resource "aws_iam_role" "configure_count_role" {
  name = "${substr(var.environment, 0, 1)}-configure-count-role"

  assume_role_policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Action": "sts:AssumeRole",
      "Principal": {
        "Service": "lambda.amazonaws.com"
      },
      "Effect": "Allow",
      "Sid": ""
    }
  ]
}
EOF
}

resource "aws_lambda_function" "configure_count" {
  # If the file is not in the current working directory you will need to include a
  # path.module in the filename.
  filename      = "ConfigureCount.zip"
  function_name = "${substr(var.environment, 0, 1)}-configure-count"
  role          = aws_iam_role.configure_count_role.arn
  handler       = "ConfigureCount"

  # The filebase64sha256() function is available in Terraform 0.11.12 and later
  # For Terraform 0.11.11 and earlier, use the base64sha256() function and the file() function:
  # source_code_hash = "${base64sha256(file("lambda_function_payload.zip"))}"
  source_code_hash = filebase64sha256("ConfigureCount.zip")

  runtime = "dotnet6"

  environment {
    variables = {
      DataArchiveBatchSize = 200
    }
  }
}