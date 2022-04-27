<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ImgUpload.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.ImgUpload" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function ()
        {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded()
        {
            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>

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


            <div class="card card-fluid container-data mt-5" style="min-height: 1000px;">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-2" id="div1" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Company
                                </label>
                                <asp:DropDownList ID="ddlCompanyAgg" runat="server" OnSelectedIndexChanged="ddlCompanyAgg_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select form-control" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2" id="div3" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Department
                                </label>
                                <asp:DropDownList ID="ddldepartmentagg" runat="server" OnSelectedIndexChanged="ddldepartmentagg_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select form-control" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="div2" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Section Name
                                </label>
                                <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Employee List
                                    
                                </label>
                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>

                        <div class="col-md-2 d-none">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Card #   
                                    <asp:LinkButton ID="imgbtnSearchEmployee" runat="server" OnClick="imgbtnEmpSeach_Click"><i class="fas fa-search "></i></asp:LinkButton>

                                </label>
                                                                       <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>

                    </div>
                     
                </div>

                <div class="card-body">
                    <asp:Panel ID="Panel2" runat="server">
                        <div class="row">
                            <div class="col-md-2">

                                <div class="form-group">
                                    <label for="tf3">Employee Image</label>
                                    <div class="custom-file">
                                        <asp:FileUpload ID="imgFileUpload" runat="server" class="custom-file-input"
                                            onchange="submitform();" ToolTip="Employee Image" />
                                        <label class="custom-file-label" for="imgFileUpload">Choose file</label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <section class="card card-figure">
                                        <!-- .card-figure -->
                                        <figure class="figure">
                                            <!-- .figure-img -->
                                            <div class="figure-img figure-attachment">
                                                <asp:HiddenField ID="Hiddnrl" runat="server" />
                                                <asp:Image ID="EmpImg" runat="server"  Width="100" Height="100" alt="Images" />

                                                <a href="#" class="img-link" data-size="1000x1000">
                                                    <span class="tile tile-circle bg-danger">
                                                        <span class="oi oi-eye"></span>
                                                    </span>
                                                    <span class="img-caption d-none">Employe Photo</span>
                                                </a>
                                            </div>
                                           
                                        </figure>
                                        <!-- /.card-figure -->
                                    </section>

                                </div>



                            </div>
                            <div class="col-md-2">

                                <div class="form-group">
                                    <label for="tf3">Employee Signature</label>
                                    <div class="custom-file">
                                        <asp:FileUpload ID="imgSigFileUpload" runat="server" class="custom-file-input"
                                            onchange="submitform();" ToolTip="Employee Image" />
                                        <label class="custom-file-label" for="imgFileUpload">Choose file</label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <section class="card card-figure">
                                        <!-- .card-figure -->
                                        <figure class="figure">
                                            <!-- .figure-img -->
                                            <div class="figure-img figure-attachment">
                                                <asp:Image ID="EmpSig" runat="server" alt="Images" />

                                                <a href="#" class="img-link" data-size="1000x1000">
                                                    <span class="tile tile-circle bg-danger">
                                                        <span class="oi oi-eye"></span>
                                                    </span>
                                                    <span class="img-caption d-none">Employee Signature</span>
                                                </a>
                                            </div>
                                            
                                        </figure>
                                        <!-- /.card-figure -->
                                    </section>

                                </div>



                            </div>
                        </div>


                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server">
                        <div class="row">

                        
                <div class="col-md-4 pading5px col-md-offset-2 text-center">
                    
                    <%--<asp:Button ID="btnGenerate" OnClick = "GenerateThumbnail" runat="server" Text="Generate Thumbnail" />--%>

                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-danger" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                    <asp:LinkButton ID="lnkbtnUpdateEMPImage" runat="server" CssClass="btn btn-success" OnClick="lnkbtnUpdateEMPImage_Click">Update</asp:LinkButton>
                    <asp:LinkButton ID="lbtnUpdateImg" runat="server" CssClass="btn btn-success" OnClick="lbtnUpdateImg_Click">Update</asp:LinkButton>

                    </div>

                </div>
            </asp:Panel>
                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


