package com.example.mobileloyaltyapp

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle

class SplashScreenActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_splash_screen)

        val background = object : Thread(){
            override fun run() {
                sleep(1000)
                val intent = Intent(baseContext, MainActivity::class.java)
                startActivity(intent)
            }
        }
        background.start()
    }
}
