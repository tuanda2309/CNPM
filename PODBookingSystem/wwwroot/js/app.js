var btn = document.querySelector('button'),
    inpt = document.querySelector('input'),
    dv = document.querySelector('.commentsDisplay > div'),
    txt = document.querySelector('textarea');

btn.onclick = function() {
    'use strict';
    if (inpt.value && txt.value != '') {
        dv.innerHTML += '<h4>' + inpt.value + '</h4>' + '<p>' + txt.value + '</p>';
        txt.value = '';
        var pp = document.querySelectorAll('p');
        for (var i = 0; i < pp.length; i++) {
            pp[i].innerHTML = pp[i].innerHTML.replace(/مبروك/gi, '<span class="ba">مبروك</span>');
            pp[i].innerHTML = pp[i].innerHTML.replace(/xoxo/gi, '<span class="lo">xoxo</span>');
            pp[i].innerHTML = pp[i].innerHTML.replace(/حب/gi, '<span class="lo">حب</span>');
            pp[i].innerHTML = pp[i].innerHTML.replace(/congrats/gi, '<span class="ba">congrats</span>');
        }
        var heart = document.querySelector('header'),
            love = document.querySelectorAll('.lo');
        for (var x = 0; x < love.length; x++) {
            love[x].onclick = function() {
                heart.classList.toggle('show');
            }
        }
        var foot = document.querySelector('footer'),
            bal = document.querySelectorAll('.ba');
        for (var z = 0; z < bal.length; z++) {
            bal[z].onclick = function() {
                foot.classList.toggle('show');
            }
        }
    }
};
