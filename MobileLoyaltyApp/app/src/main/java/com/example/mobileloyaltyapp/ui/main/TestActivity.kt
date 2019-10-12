package com.example.mobileloyaltyapp.ui.main

import android.content.Intent
import android.os.Bundle
import android.widget.ImageView
import com.google.android.material.snackbar.Snackbar
import androidx.appcompat.app.AppCompatActivity
import com.example.mobileloyaltyapp.MainActivity
import com.example.mobileloyaltyapp.R

import kotlinx.android.synthetic.main.activity_test.*

class TestActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_test)

        var button = findViewById<ImageView>(R.id.backButton);
        button.setOnClickListener{
            var intent = Intent(applicationContext, MainActivity::class.java)
            startActivity(intent)
        }


    }

}
