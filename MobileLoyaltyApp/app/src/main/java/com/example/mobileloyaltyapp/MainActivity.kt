package com.example.mobileloyaltyapp

import android.app.PendingIntent.getActivity
import android.net.Uri
import android.os.Bundle
import com.google.android.material.floatingactionbutton.FloatingActionButton
import com.google.android.material.snackbar.Snackbar
import com.google.android.material.tabs.TabLayout
import androidx.viewpager.widget.ViewPager
import androidx.appcompat.app.AppCompatActivity
import android.view.Menu
import android.view.MenuItem
import android.view.View
import android.widget.ImageView
import android.widget.LinearLayout
import com.example.mobileloyaltyapp.fragments.AdsFragment
import com.example.mobileloyaltyapp.fragments.BlankFragment
import com.example.mobileloyaltyapp.fragments.HistotyFragment
import com.example.mobileloyaltyapp.fragments.ProfileFragment
import com.example.mobileloyaltyapp.ui.main.SectionsPagerAdapter
import com.squareup.picasso.Picasso

class MainActivity : AppCompatActivity(),
    BlankFragment.OnFragmentInteractionListener,
    ProfileFragment.OnFragmentInteractionListener,
    HistotyFragment.OnFragmentInteractionListener,
    AdsFragment.OnFragmentInteractionListener {

    override fun onFragmentInteraction(uri: Uri) {
        TODO("not implemented") //To change body of created functions use File | Settings | File Templates.
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        val sectionsPagerAdapter = SectionsPagerAdapter(this, supportFragmentManager)
        val viewPager: ViewPager = findViewById(R.id.view_pager)
        viewPager.adapter = sectionsPagerAdapter
        val tabs: TabLayout = findViewById(R.id.tabs)
        tabs.setupWithViewPager(viewPager)
        val linearLayout : LinearLayout = findViewById(R.id.qr_layot)
        val fab: FloatingActionButton = findViewById(R.id.fab)

        val qrImage : ImageView = findViewById(R.id.image_qr)
        var imageUrl : String = "http://mobileloyaltyapi.gear.host/api/ui/code"
        var open : Boolean = false

        fab.setOnClickListener { view ->
            if (open)
            {
                linearLayout.visibility = View.INVISIBLE
                open = false
            }
            else {
                linearLayout.visibility = View.VISIBLE

                Picasso.with(applicationContext)
                    .load(imageUrl)
                    .resize(600, 600)
                    .into(qrImage)
                open = true
            }
        }

    }
}