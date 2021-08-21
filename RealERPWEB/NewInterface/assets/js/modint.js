$(function() {
    $('[data-toggle="tooltip"]').tooltip()
  // Sidebar toggle behavior
  $('#sidebarCollapse').on('click', function() {
    $('#sidebar, #content').toggleClass('active')    
         
    });
});