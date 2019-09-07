package com.example.mobileloyaltyapp.models

data class Profile (
    val UserName : String,
    val Balance : Double,
    val Transactions : Array<String>
)