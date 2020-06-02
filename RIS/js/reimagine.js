jQuery(document).ready(function(){
    $('.progressbar li').on('click',function(){
        $('.progressbar li').removeClass('active');
        $(this).addClass('active');
        $('#progressbarval').val($(this).text().trim());
    });
})