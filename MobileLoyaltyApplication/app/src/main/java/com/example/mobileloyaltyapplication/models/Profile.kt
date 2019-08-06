package com.example.mobileloyaltyapplication.models

data class Profile (
    val UserName : String,
    val Balance : Double,
    val Transactions : Array<String>
)