<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AcceessError.aspx.cs" Inherits="RealERPWEB.AcceessError" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style>
    .state-header {
    animation: 1s ease 0s alternate none infinite running color_change;
    color: #ffffff;
    padding: 5px 35px;
    font-size:20px;
}
    .state-header{
        font-size:25px;
    }
@keyframes color_change {
0% {
    background-color: blue;
}
100% {
    background-color: red;
}
}
@keyframes color_change {
0% {
    background-color: blue;
}
100% {
    background-color: red;
}
}

</style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

      <!-- .empty-state -->
          <section class="empty-state">
            <!-- .empty-state-container -->
            <div class="empty-state-container">
              <div class="state-figure">
                <img class="img-fluid" src="images/illustration/img-2.png" alt="" style="max-width: 320px"> </div>
              <h3 class="state-header badge badge-lg badge-danger"><span class="oi oi-media-record pulse mr-1"></span> Page Not found! </h3>
              <p class="state-description lead text-muted"> Sorry, we've misplaced that URL or it's pointing to something that doesn't exist. </p>
              <div class="state-action">
                    Contact your administrator, or please send your Query, <a class="badge badge-lg badge-danger" href="mailto:info@pintechltd.bd">info@pintechltd.com</a> 
               
              </div>
            </div>
            <!-- /.empty-state-container -->
          </section>
          <!-- /.empty-state -->
 
  
    


</asp:Content>


