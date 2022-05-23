<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ImgUpload.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.ImgUpload" %>

<asp:Content ID="Content" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }



    </script>
      <script type="text/javascript">
          function showImagePreview(input) {
              document.getElementById("imgPreview").style.display = "block";
              if (input.files && input.files[0]) {
                  var filerdr = new FileReader();
                  filerdr.onload = function (e) {
                      $('#imgPreview').attr('src', e.target.result);
                  }
                  filerdr.readAsDataURL(input.files[0]);
              }
          }

          function showImagePreview1(input) {
              document.getElementById("imgPreview1").style.display = "block";
              if (input.files && input.files[0]) {
                  var filerdr = new FileReader();
                  filerdr.onload = function (e) {
                      $('#imgPreview1').attr('src', e.target.result);
                  }
                  filerdr.readAsDataURL(input.files[0]);
              }
          }
      </script>



    <style>
        .chzn-container {
            width:100%!important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>


            <div class="card mt-5">

                <div class="card-header">
              
                            <div class="row">
     
                        <div class="col-lg-3">


                            <div class="form-group">
                                <label for="ddlLvType">
                                    Company
                                </label>
                                <asp:DropDownList ID="ddlCompanyAgg" runat="server" OnSelectedIndexChanged="ddlCompanyAgg_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select form-control" TabIndex="2">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Department
                                </label>
                                <asp:DropDownList ID="ddldepartmentagg" runat="server" OnSelectedIndexChanged="ddldepartmentagg_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select form-control" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Section Name
                                </label>
                                <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Employee List
                                 <asp:LinkButton ID="empSrc" runat="server" OnClick="empSrc_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                </label>
                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>

                      
              

                    </div>
                    <div class="row">
                          <div class="col-lg-6">

                            <div class="form-group">
                                <label for="tf3">Employee Image</label>
                                <div class="custom-file">
                                    <asp:FileUpload ID="imgFileUpload" runat="server" class="custom-file-input" ToolTip="Employee Image" AllowMultiple="true" onchange="showImagePreview(this)"/>
                                    <label class="custom-file-label" for="imgFileUpload">Choose file</label>
                                </div>
                            </div>
                         <img id="imgPreview" alt="Preview image" height="100" style="display:none;"/>

                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="tf3">Employee Signature</label>
                                <div class="custom-file">
                                    <asp:FileUpload ID="imgSigFileUpload" runat="server" class="custom-file-input" ToolTip="Employee Image" AllowMultiple="true" onchange="showImagePreview1(this)" />
                                    <label class="custom-file-label" for="imgFileUpload">Choose file</label>
                                </div>
                          
                            </div>
                                <img id="imgPreview1" alt="Preview image"   height="100" style="display:none;"/>
                        </div>

                                  <div class="col-lg-12 mb-2 text-center">
                            <asp:LinkButton ID="lnkbtnUpdateEMPImage" runat="server" CssClass="btn btn-success " OnClick="lnkbtnUpdateEMPImage_Click" style="margin-top:28px;">Save</asp:LinkButton>
                        </div>
                    </div>
            
                </div>

                <div class="card-body">
                                              <div class="col-md-2 d-none">
                            <div class="form-group">
                                <label for="ddlLvType">
                               

                                </label>
                                 <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                    <div class="table table-bordered ">
                        <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvimg" runat="server" AutoGenerateColumns="false">
                            <Columns>
                 
                                <asp:TemplateField HeaderText="Image ">
                                    <ItemTemplate>
                                                <asp:Label ID="lblimg" runat="server" Text='<%#Eval("imgurl")%>' Visible="false"></asp:Label>
                                                      <asp:Label ID="lblsign" runat="server" Text='<%#Eval("signurl")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblid" runat="server" Text='<%#Eval("empid")%>' Visible="false"></asp:Label>

                                        <asp:HyperLink runat="server" NavigateUrl='<%#Eval("imgurl")%>' Target="_blank">
                                                <asp:Image Width="100px" Height="100px" runat="server" ImageUrl ='<%#Eval("imgurl")%>'/>
                                        </asp:HyperLink>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Signature ">
                                    <ItemTemplate>

                                        <asp:HyperLink runat="server"  NavigateUrl='<%#Eval("signurl")%>' Target="_blank">
                                            <asp:Image Width="100px" Height="100px" runat="server" ImageUrl ='<%#Eval("signurl")%>'/>
                                        </asp:HyperLink>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_remove" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btn_remove_Click1"> <i class="fa fa-trash"></i> 
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                 
            </div>

                </div>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkbtnUpdateEMPImage" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


