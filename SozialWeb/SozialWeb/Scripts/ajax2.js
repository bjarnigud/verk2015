/* Aðferð 1, notar sama controller */

// Hengjum aðgerð á document ready event með jQuery
$(document).ready(function () {

    // Ágætt að venja sig á að nota "event delegation", þ.e .on() fallið í jQuery, en það hengir event á það sem er
    // akkúrat núna í DOM trénu í skjalinu og *einnig* öll element sem gætu bæst við síðar á dýnamískan máta
    // Hér hengjum við submit event á öll form í skjalinu (sem er bara eitt eins og er)
    $('form').submit(function () {
        // Inn í þessum submit event handler er $(this) vísun í form tagið sjálft.  Geymum reference á það í breytunni "theForm"
        var theForm = $(this);

        // Framkvæmum okkar eigin Async POST aðgerð (AJAX) með jQuery .ajax() fallinu
        $.ajax({
            type: 'POST',
            processData: false,
            //url: '/Profile/AddPost',
            url: theForm.attr('action'), // Í stað þess að harðkóða inn slóðina, þá lesum við einfaldlega slóðina sem formið er að vísa á by-default (action eigindið).
            //url: theForm.attr('action'), // Í stað þess að harðkóða inn slóðina, þá lesum við einfaldlega slóðina sem formið er að vísa á by-default (action eigindið).
            data: theForm.serialize(), // .serialize() aðgerðin les allar upplýsingar úr forminu og býr til query-string úr því, þ.e &name1=value1&name2=value2 etc.
        }).done(function (result) {

            // result í þessu tilviki er allt svarið frá controllernum og þar sem við erum að nota óbreyttan controller, þá erum við að fá
            // allt HTML-ið til baka.  Við viljum hinsvegar bara fá þann part sem er inn í div-inu sem er með ID = comments-list.
            // Hér sendum við HTML-ið inn í jQuery object sem parsar það fyrir okkur og þá getum við notað .find() til að velja bara div-ið sem er með ID = comments-list
            var resultHtml = $(result).find('#ajax-2');
            //console.log("This is the resultHtml" + resultHtml);
            //console.log("This is the result" + result);
            // Tökum svo núverandi DIV með ID = comments-list og gerum .replaceWith(), þ.e skiptum því og öllu innihaldinu út fyrir nýja HTML-ið sem er þá með nýjustu commentum
            // og þ.a.l líka commentinu sem við vorum að vista inn
            $('#ajax-2').replaceWith(resultHtml);
            console.log(theForm.serialize());


            // Loks þurfum við að tæma innsláttarreitinn, því hann er ekki hluti af því HTML-i sem við erum að uppfæra.
            theForm.find('input[type=text]').val('');
        }).fail(function () {
            alert('Villa kom upp!');
        });

        // Verðum að muna að gera return false til að koma í veg fyrri að vafrinn framkvæmi default aðgerðina þegar
        // smellt er á submit hnappinn, en það er einmitt að submit-a forminu sem veldur því að öll síðan endurhleðst.
        // Það er einmitt það sem við erum að reyna að koma í veg fyrir að gerist.
        return false;
    });
});