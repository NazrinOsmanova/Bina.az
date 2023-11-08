document.querySelectorAll(".section11 .container .iki .asagi .items img").forEach(x => {
    x.addEventListener("click", function () {
        document.querySelector(".section11 .container .iki .asagi .sekil img").src = x.src.replace("/small","")
    })
});


document.querySelectorAll(".fa-heart").forEach(x => {
    x.addEventListener("click", function () {
        x.classList.toggle("qirmizi");
        document.querySelector(".section7 .container div p").classList.add("css1");
    })
});


document.querySelector('[name="ProductMenzilId"]').addEventListener('change', function () {
    funkHesabla()
})


function funkHesabla() {
    if (document.querySelector('[name="ProductMenzilId"]').value == 1 || document.querySelector('[name="ProductMenzilId"]').value == 2 || document.querySelector('[name="ProductMenzilId"]').value == 3) {
        document.querySelector(".section6 .container .elan .sol .otaqSayi").classList.remove('hideinput')
        document.querySelector(".section6 .container .elan .sol .mertebe").classList.remove('hideinput')
        document.querySelector(".section6 .container .elan .sol .mertebeSayi").classList.remove('hideinput')
        document.querySelector(".section6 .container .elan .sol .temir").classList.remove('hideinput')
        document.querySelector(".section6 .container .elan .sol .torpaqSahesi").classList.add('hideinput')
    }
    else if (document.querySelector('[name="ProductMenzilId"]').value == 4) {
        document.querySelector(".section6 .container .elan .sol .mertebe").classList.add('hideinput')
        document.querySelector(".section6 .container .elan .sol .mertebeSayi").classList.add('hideinput')
        document.querySelector(".section6 .container .elan .sol .otaqSayi").classList.remove('hideinput')
        document.querySelector(".section6 .container .elan .sol .temir").classList.remove('hideinput')
        document.querySelector(".section6 .container .elan .sol .torpaqSahesi").classList.remove('hideinput')
    }
    else if (document.querySelector('[name="ProductMenzilId"]').value == 5) {
        document.querySelector(".section6 .container .elan .sol .otaqSayi").classList.add('hideinput')
        document.querySelector(".section6 .container .elan .sol .temir").classList.remove('hideinput')
        document.querySelector(".section6 .container .elan .sol .torpaqSahesi").classList.remove('hideinput')
    }
    else if (document.querySelector('[name="ProductMenzilId"]').value == 6) {
        document.querySelector(".section6 .container .elan .sol .mertebe").classList.add('hideinput')
        document.querySelector(".section6 .container .elan .sol .mertebeSayi").classList.add('hideinput')
        document.querySelector(".section6 .container .elan .sol .torpaqSahesi").classList.add('hideinput')
        document.querySelector(".section6 .container .elan .sol .temir").classList.remove('hideinput')
        document.querySelector(".section6 .container .elan .sol .otaqSayi").classList.remove('hideinput')
    }
    else if (document.querySelector('[name="ProductMenzilId"]').value == 7 || document.querySelector('[name="ProductMenzilId"]').value == 8 || document.querySelector('[name="ProductMenzilId"]').value == 9) {
        document.querySelector(".section6 .container .elan .sol .mertebe").classList.add('hideinput')
        document.querySelector(".section6 .container .elan .sol .mertebeSayi").classList.add('hideinput')
        document.querySelector(".section6 .container .elan .sol .torpaqSahesi").classList.add('hideinput')
        document.querySelector(".section6 .container .elan .sol .temir").classList.remove('hideinput')
        document.querySelector(".section6 .container .elan .sol .otaqSayi").classList.remove('hideinput')
    }
}


document.querySelector('[name="ProductAlisKirayeId"]').addEventListener('change', function () {
    if (this.value == 1) {

        document.querySelector(".section6 .container .elan .sol .gunay").classList.add('hideinput')
    }
    else if (this.value == 2) {
        document.querySelector(".section6 .container .elan .sol .gunay").classList.remove('hideinput')
    }
})


window.onload = () => {
    funkHesabla()
}



document.querySelector('.div10 input[type="file"]').addEventListener('change', function () {
    var file = document.querySelector('.div10 input[type="file"]').files[0];
    var reader = new FileReader();

    reader.addEventListener("load", function () {
        document.querySelector(".section16 .container form .sol img").src = reader.result;
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
});