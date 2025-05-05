# Test script for MyTimeoutApp
Write-Host "Testing the timeout functionality..." -ForegroundColor Cyan

# Step 1: Send a command to start a timeout operation
Write-Host "Step 1: Starting a timeout operation..." -ForegroundColor Yellow
$startResponse = Invoke-RestMethod -Uri "http://localhost:5000/request/start" -Method Post -ContentType "application/json" -Body '{"payload":"test data"}'
Write-Host "Response: $startResponse" -ForegroundColor Green
Write-Host "Timeout operation started. It will timeout after 10 seconds if not completed." -ForegroundColor Yellow

# Prompt user to choose what to do
Write-Host ""
Write-Host "Options:" -ForegroundColor Cyan
Write-Host "1. Wait for timeout (10 seconds)" -ForegroundColor White
Write-Host "2. Complete the operation manually" -ForegroundColor White
$choice = Read-Host "Enter your choice (1 or 2)"

if ($choice -eq "2") {
    # Get correlation ID from console output
    Write-Host "Please enter the correlation ID from the console output:" -ForegroundColor Yellow
    $correlationId = Read-Host "Correlation ID"
    
    # Step 2: Complete the operation manually
    Write-Host "Step 2: Completing the operation manually..." -ForegroundColor Yellow
    $completeResponse = Invoke-RestMethod -Uri "http://localhost:5000/request/complete/$correlationId" -Method Post -ContentType "application/json" -Body '"completed manually"'
    Write-Host "Response: $completeResponse" -ForegroundColor Green
} else {
    # Wait for timeout
    Write-Host "Waiting for timeout (10 seconds)..." -ForegroundColor Yellow
    Start-Sleep -Seconds 12
    Write-Host "Timeout should have occurred. Check the console output." -ForegroundColor Green
}

Write-Host ""
Write-Host "Test completed." -ForegroundColor Cyan 